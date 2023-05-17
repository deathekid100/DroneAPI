using DronesAPI.Dtos;
using DronesAPI.Models;
using FluentValidation.Results;

namespace DronesAPI.Services
{
    public interface IDispatchService
    {
        Task<List<Medication>> GetAllMedicationsFromIds(List<int> medicationsIds);
        Task<ICollection<ReadDroneDto>> GetAvailableDrones();
        Task<ReadDroneDto> GetDroneById(int id);
        Task<ICollection<ReadMedicationsDto>> GetMedications(int droneId);
        Task<ICollection<ReadMedicationsDto>> LoadDroneWithMedications(int droneId, List<Medication> medications);
        Task<ReadDroneDto> RegisterDrone(CreateDroneDto createDrone);
        Task<ValidationResult?> ValidateDrone(CreateDroneDto createDrone);
    }
}
