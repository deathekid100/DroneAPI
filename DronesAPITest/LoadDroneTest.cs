using DronesAPI.Dtos;
using DronesAPI.Models;
using FluentAssertions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DronesAPITest
{
    public class LoadDroneTest : TestInitialize
    {
        [Fact]
        public async Task LoadDrone_ValidData_ShouldReturnsCreatedStatusCodeAndModelResponse()
        {
            // Arrange
            var drone = dbContext.Drones.FirstOrDefault(t => t.State == DroneState.IDLE && t.BatteryCapacity > 25 && t.WeightLimit == 500);

            var medicationsIds = dbContext.Medications
                .Where(t => t.Weight < 500)
                .Take(1)
                .Select(t => t.Id)
                .ToList();

            var content = new StringContent(
                JsonSerializer.Serialize(medicationsIds),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _httpClient.PostAsync($"api/v1/drone/{drone.Id}/load", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<List<ReadMedicationsDto>>(responseBody, _jsonOptions);

            modelResponse.Should().NotBeNull();
            modelResponse.Should().HaveCount(1);
            modelResponse.Should().OnlyContain(m => medicationsIds.Contains(m.Id));
        }
        [Fact]
        public async Task LoadInvalidDrone_InvalidData_ShouldReturnsNotFoundError()
        {
            // Arrange

            var droneId = -1;
            var medicationsIds = dbContext.Medications
                .Where(t => t.Weight < 500)
                .Take(1)
                .Select(t => t.Id)
                .ToList();

            var content = new StringContent(
                JsonSerializer.Serialize(medicationsIds),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _httpClient.PostAsync($"api/v1/drone/{droneId}/load", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task LoadInUseDrone_InvalidData_ShouldReturnsBadRequestDroneIsInUse()
        {
            // Arrange
            var drone = dbContext.Drones.FirstOrDefault(t => t.State != DroneState.IDLE);
            var medicationsIds = dbContext.Medications
                .Where(t => t.Weight <= drone.WeightLimit)
                .Take(1)
                .Select(t => t.Id)
                .ToList();

            var content = new StringContent(
                JsonSerializer.Serialize(medicationsIds),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _httpClient.PostAsync($"api/v1/drone/{drone.Id}/load", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<string>(responseBody, _jsonOptions);
            modelResponse.Should().Be("Drone is being used");
        }
        [Fact]
        public async Task LoadDroneWeightLimit_InvalidData_ShouldReturnsBadRequestWeightLimitExceeded()
        {
            // Arrange
            var drone = dbContext.Drones.FirstOrDefault(t => t.State == DroneState.IDLE && t.BatteryCapacity > 25);
            var medicationsIds = dbContext.Medications
                .Select(t => t.Id)
                .ToList();

            var content = new StringContent(
                JsonSerializer.Serialize(medicationsIds),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _httpClient.PostAsync($"api/v1/drone/{drone.Id}/load", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<string>(responseBody, _jsonOptions);
            modelResponse.Should().Be("Drone weight limit exceeded");
        }
        [Fact]
        public async Task LoadDroneBatteryLow_InvalidData_ShouldReturnsBadRequestBatteryLow()
        {
            // Arrange
            var drone = dbContext.Drones.FirstOrDefault(t => t.State == DroneState.IDLE && t.BatteryCapacity < 25 && t.WeightLimit == 500);
            var medicationsIds = dbContext.Medications
                .Where(t => t.Weight <= drone.WeightLimit)
                .Take(1)
                .Select(t => t.Id)
                .ToList();

            var content = new StringContent(
                JsonSerializer.Serialize(medicationsIds),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _httpClient.PostAsync($"api/v1/drone/{drone.Id}/load", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<string>(responseBody, _jsonOptions);
            modelResponse.Should().Be("Battery level is below 25%");
        }
        [Fact]
        public async Task LoadDrone_InvalidMedications_ShouldReturnsBadRequestMedicationsNotFound()
        {
            // Arrange
            var drone = dbContext.Drones.FirstOrDefault(t => t.State == DroneState.IDLE && t.BatteryCapacity > 25);
            var medicationsIds = new List<int>() { -1 };

            var content = new StringContent(
                JsonSerializer.Serialize(medicationsIds),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _httpClient.PostAsync($"api/v1/drone/{drone.Id}/load", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<string>(responseBody, _jsonOptions);
            modelResponse.Should().Be("One or more medications are not found");
        }
    }
}
