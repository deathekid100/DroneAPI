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
    public class BaterryLevelTest
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonOptions;

        public BaterryLevelTest()
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
        public async Task GetBatteryLevelForADrone_ValidData_ShouldReturnsTheBatteryLevel()
        {
            // Arrange
            var createDroneDto = new CreateDroneDto
            {
                SerialNumber = "AAAA",
                BatteryCapacity = 100,
                Model = DroneModel.Lightweight,
                WeightLimit = 500,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(createDroneDto),
                Encoding.UTF8,
                "application/json"
            );
            var createdResponse = await _httpClient.PostAsync($"api/v1/drone", content);
            var responseString = await createdResponse.Content.ReadAsStringAsync();
            var droneResponse = JsonSerializer.Deserialize<ReadDroneDto>(responseString, _jsonOptions);

            // Act
            var response = await _httpClient.GetAsync($"api/v1/drone/{droneResponse.Id}/BatteryLevel");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<int>(responseBody, _jsonOptions);
            modelResponse.Should().Be(100);
        }
        [Fact]
        public async Task GetBatteryLevelForADrone_InvalidData_ShouldReturnsNotFound()
        {
            // Arrange
            var droneId = -1;

            // Act
            var response = await _httpClient.GetAsync($"api/v1/drone/{droneId}/BatteryLevel");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
