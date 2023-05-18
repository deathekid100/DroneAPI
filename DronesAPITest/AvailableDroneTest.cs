using DronesAPI.Dtos;
using DronesAPI.Models;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace DronesAPITest
{
    public class AvailableDroneTest : TestInitialize
    {
        [Fact]
        public async Task GetAvailableDrones_ShouldReturnsAvailableDrones()
        {
            var available = dbContext.Drones.Where(t => t.State == DroneState.IDLE && t.BatteryCapacity >= 25).ToList();

            // Act
            var response = await _httpClient.GetAsync("api/v1/drone/available");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<List<ReadDroneDto>>(responseBody, _jsonOptions);

            modelResponse.Should().NotBeNull();
            modelResponse.Should().HaveCount(available.Count);
            modelResponse.Should().OnlyContain(m => available.Any(t => t.Id == m.Id));
        }
    }
}
