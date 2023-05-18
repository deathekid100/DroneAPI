using DronesAPI.Dtos;
using DronesAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DronesAPITest
{
    public class AvailableDroneTest
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonOptions;

        public AvailableDroneTest()
        {
            var application = new WebApplicationFactory<Program>();
            _httpClient = application.CreateDefaultClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() }
            };
        }
        [Fact]
        public async Task GetAvailableDrones_ShouldReturnsAvailableDrones()
        {
            // Arrange
            var drone1 = new CreateDroneDto
            {
                SerialNumber = "A1",
                BatteryCapacity = 100,
                Model = DroneModel.Lightweight,
                WeightLimit = 500,
            };
            var drone2 = new CreateDroneDto
            {
                SerialNumber = "A2",
                BatteryCapacity = 25,
                Model = DroneModel.Lightweight,
                WeightLimit = 500,
            };

            var content1 = new StringContent(
                JsonSerializer.Serialize(drone1),
                Encoding.UTF8,
                "application/json"
            );
            var content2 = new StringContent(
                JsonSerializer.Serialize(drone2),
                Encoding.UTF8,
                "application/json"
            );
            await _httpClient.PostAsync("api/v1/drone", content1);
            await _httpClient.PostAsync("api/v1/drone", content2);

            // Act
            var response = await _httpClient.GetAsync("api/v1/drone/available");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<List<ReadDroneDto>>(responseBody, _jsonOptions);

            modelResponse.Should().NotBeNull();
            modelResponse.Should().HaveCountGreaterThanOrEqualTo(1);
            modelResponse.Should().Contain(p => p.SerialNumber == "A1");
            modelResponse.Should().NotContain(p => p.SerialNumber == "A2");
            modelResponse.Should().OnlyContain(p => p.State == DroneState.IDLE);
            modelResponse.Should().OnlyContain(p => p.BatteryCapacity > 25);
        }
    }
}
