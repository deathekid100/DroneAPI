using DronesAPI.Dtos;
using DronesAPI.Models;
using FluentValidation.Results;

namespace DronesAPI.Services
{
    public interface IDispatchService
    {
        Task<ReadDroneDto> GetDroneById(int id);
        Task<ReadDroneDto> RegisterDrone(CreateDroneDto createDrone);
        Task<ValidationResult?> ValidateDrone(CreateDroneDto createDrone);
    }
}
