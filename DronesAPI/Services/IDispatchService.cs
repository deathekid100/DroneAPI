using DronesAPI.Dtos;
using DronesAPI.Models;
using FluentValidation.Results;

namespace DronesAPI.Services
{
    public interface IDispatchService
    {
        Task<bool> CheckMedications(List<ReadMedicationsDto> medications);
        Task<ICollection<ReadDroneDto>> GetAvailableDrones();
        Task<ReadDroneDto> GetDroneById(int id);
        Task<ICollection<ReadDroneDto>> GetMedications(int droneId);
        Task<Drone> LoadDroneWithMedications(Drone drone, List<ReadMedicationsDto> medications);
        Task<ReadDroneDto> RegisterDrone(CreateDroneDto createDrone);
        Task<ValidationResult?> ValidateDrone(CreateDroneDto createDrone);
    }
}
