using DronesAPI.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DronesAPITest
{
    public class MedicationsTest
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonOptions;

        public MedicationsTest()
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
        public async Task GetMedicationsForADrone_ValidData_ShouldReturnsAListOfMedications()
        {
            // Arrange
            var droneId = 1;
            var medicationsIds = new List<int>() { 1, 2 };

            var contentMedications = new StringContent(
                JsonSerializer.Serialize(medicationsIds),
                Encoding.UTF8,
                "application/json"
            );
            await _httpClient.PostAsync($"api/v1/drone/{droneId}/load", contentMedications);

            // Act
            var response = await _httpClient.GetAsync($"api/v1/drone/{droneId}/medications");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<List<ReadMedicationsDto>>(responseBody, _jsonOptions);
            
            modelResponse.Should().NotBeNull();
            modelResponse.Should().HaveCount(2);

            var ids = modelResponse.Select(t => t.Id);
            
            ids.Should().Contain(1);
            ids.Should().Contain(2);
        }
        [Fact]
        public async Task GetMedicationsForADrone_InvalidData_ShouldReturnsNotFound()
        {
            // Arrange
            var droneId = 100;

            // Act
            var response = await _httpClient.GetAsync($"api/v1/drone/{droneId}/medications");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
