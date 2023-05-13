using AutoMapper;
using DronesAPI.Data;
using DronesAPI.Dtos;
using DronesAPI.Models;
using DronesAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DroneDBContext>(options =>
    options.UseInMemoryDatabase(databaseName: "DronesDB"));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(DroneValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(MedicationValidator));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
