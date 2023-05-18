using DronesAPI.Dtos;
using DronesAPI.Models;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace DronesAPITest
{
    public class MedicationsTest : TestInitialize
    {
        [Fact]
        public async Task GetMedicationsForADrone_ValidData_ShouldReturnsAListOfMedications()
        {
            var drone = dbContext.Drones.FirstOrDefault(t => t.State == DroneState.IDLE && t.WeightLimit == 500 && t.BatteryCapacity > 25);
            var medications = dbContext.Medications
               .Where(t => t.Weight < 500)
               .Take(1)
               .ToList();
            drone.Medications = medications;
            dbContext.Drones.Update(drone);
            dbContext.SaveChanges();

            // Act
            var response = await _httpClient.GetAsync($"api/v1/drone/{drone.Id}/medications");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<List<ReadMedicationsDto>>(responseBody, _jsonOptions);

            modelResponse.Should().NotBeNull();
            modelResponse.Should().HaveCount(1);
            modelResponse.Should().OnlyContain(m => medications.Any(t => t.Id == m.Id));
        }
        [Fact]
        public async Task GetMedicationsForADrone_InvalidData_ShouldReturnsNotFound()
        {
            // Arrange
            var droneId = -1;

            // Act
            var response = await _httpClient.GetAsync($"api/v1/drone/{droneId}/medications");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
