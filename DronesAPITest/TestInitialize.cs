using DronesAPI.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DronesAPITest
{
    public abstract class TestInitialize
    {
        public HttpClient _httpClient;
        public JsonSerializerOptions _jsonOptions;
        private WebApplicationFactory<Program> application;
        public DroneDBContext dbContext;
        public TestInitialize()
        {
            application = new WebApplicationFactory<Program>();
            application = application.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<DataSeederFake>();
                });
            });
            var scope = application.Services.CreateScope();
            dbContext = scope.ServiceProvider.GetRequiredService<DroneDBContext>();

            var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeederFake>();
            dataSeeder.SeedData();

            _httpClient = application.CreateDefaultClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() }
            };
        }
    }
}
