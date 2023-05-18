using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace DronesAPITest
{
    public class BaterryLevelTest : TestInitialize
    {
        [Fact]
        public async Task GetBatteryLevelForADrone_ValidData_ShouldReturnsTheBatteryLevel()
        {
            // Arrange
            var drone = dbContext.Drones.FirstOrDefault();

            // Act
            var response = await _httpClient.GetAsync($"api/v1/drone/{drone.Id}/BatteryLevel");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<int>(responseBody, _jsonOptions);
            modelResponse.Should().Be(drone.BatteryCapacity);
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
