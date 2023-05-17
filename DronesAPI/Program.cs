using DronesAPI.Data;
using DronesAPI.Dtos;
using DronesAPI.Jobs;
using DronesAPI.Models;
using DronesAPI.Services;
using DronesAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DroneDBContext>(options =>
    options.UseInMemoryDatabase(databaseName: "DronesDB"));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(DroneValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(MedicationValidator));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IDispatchService, DispatchService>();
builder.Services.AddTransient<IAuditService, AuditService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<AuditJob>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DroneDBContext>();
    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    dataSeeder.SeedData();
}


app.MapPost("api/v1/drone", async (IDispatchService dispatchService, CreateDroneDto createDrone) =>
{
    var validation = await dispatchService.ValidateDrone(createDrone);
    if (!validation!.IsValid)
    {
        return Results.ValidationProblem(validation.ToDictionary());
    }
    var result = await dispatchService.RegisterDrone(createDrone);

    return Results.Created($"api/v1/drone/{result.Id}", result);
});

app.MapGet("api/v1/drone/{id}/medications", async (IDispatchService dispatchService, int id) =>
{
    var drone = await dispatchService.GetDroneById(id);
    if (drone == null)
    {
        return Results.NotFound();
    }
    var medications = await dispatchService.GetMedications(id);
    
    return Results.Ok(medications);
});

app.MapGet("api/v1/drone/{id}/BatteryLevel", async (IDispatchService dispatchService, int id) =>
{
    var drone = await dispatchService.GetDroneById(id);
    if (drone == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(drone.BatteryCapacity);
});

app.MapPost("api/v1/drone/{id}/load", async (IDispatchService dispatchService, List<int> medicationsIds, int id) =>
{
    var drone = await dispatchService.GetDroneById(id);
    if (drone == null)
    {
        return Results.NotFound();
    }
    if (drone.State != DroneState.IDLE)
    {
        return Results.BadRequest("Drone is being used");
    }
    var medications = await dispatchService.GetAllMedicationsFromIds(medicationsIds);
    double totalWeight = medications.Sum(m => m.Weight);

    if (drone.WeightLimit < totalWeight)
    {
        return Results.BadRequest("Drone weight limit exceeded");
    }
    if (drone.BatteryCapacity < 25)
    {
        return Results.BadRequest("Battery level is below 25%");
    }

    if (medications.Count != medicationsIds.Count)
    {
        return Results.BadRequest("One or more medications are not found");
    }
    var result = await dispatchService.LoadDroneWithMedications(id, medications);
    
    return Results.Created($"api/v1/drone/{id}/medications", result);
});

app.MapGet("api/v1/drone/available", async (IDispatchService dispatchService) =>
{
    var drones = await dispatchService.GetAvailableDrones();
    return Results.Ok(drones);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
