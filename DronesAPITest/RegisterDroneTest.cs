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
    public class RegisterDroneTest
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonOptions;

        public RegisterDroneTest()
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
        public async Task RegisterDrone_ValidData_ShouldReturnsCreatedStatusCodeAndModelResponse()
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

            // Act
            var response = await _httpClient.PostAsync("api/v1/drone", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseBody = await response.Content.ReadAsStringAsync();
            var modelResponse = JsonSerializer.Deserialize<ReadDroneDto>(responseBody, _jsonOptions);

            modelResponse.Should().NotBeNull();
            modelResponse.Id.Should().BeGreaterThan(0);
            modelResponse.SerialNumber.Should().Be(createDroneDto.SerialNumber);
            modelResponse.BatteryCapacity.Should().Be(createDroneDto.BatteryCapacity);
            modelResponse.Model.Should().Be(createDroneDto.Model);
            modelResponse.WeightLimit.Should().Be(createDroneDto.WeightLimit);
            modelResponse.State.Should().Be(DroneState.IDLE);
        }
        [Fact]
        public async Task RegisterDrone_InvalidData_ShouldReturnsValidationProblem()
        {
            // Arrange
            var drone = new CreateDroneDto
            {
                BatteryCapacity = 150,
                SerialNumber = "",
                WeightLimit = -1
            };
            var content = new StringContent(
                JsonSerializer.Serialize(drone),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _httpClient.PostAsync("api/v1/drone", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/problem+json");
            var result = await response.Content.ReadAsStringAsync();
            result.Should().NotBeNull();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(result);
            problemDetails.Should().NotBeNull();
            problemDetails.Title.Should().Be("One or more validation errors occurred.");
            problemDetails.Status.Should().Be((int)HttpStatusCode.BadRequest);
            var Errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(((JsonElement)problemDetails.Extensions["errors"]).ToString());
            Errors.Should().HaveCount(4);
            Errors.Should().ContainKey("SerialNumber");
            Errors.Should().ContainKey("Model");
            Errors.Should().ContainKey("WeightLimit");
            Errors.Should().ContainKey("Model");
        }
    }
}
