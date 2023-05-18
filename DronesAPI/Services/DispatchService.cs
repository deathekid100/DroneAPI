using AutoMapper;
using DronesAPI.Data;
using DronesAPI.Dtos;
using DronesAPI.Models;
using FluentValidation;

namespace DronesAPI.Services
{
    public class DispatchService : IDispatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<Drone> _validator;

        public DispatchService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<Drone> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<ReadDroneDto> RegisterDrone(CreateDroneDto createDrone)
        {
            var drone = _mapper.Map<Drone>(createDrone);
            await _unitOfWork.DroneRepository.AddAsync(drone);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ReadDroneDto>(drone);
        }
        public async Task<FluentValidation.Results.ValidationResult?> ValidateDrone(CreateDroneDto createDrone)
        {
            var drone = _mapper.Map<Drone>(createDrone);
            var validation = await _validator.ValidateAsync(drone);
            return validation;
        }
        public async Task<ReadDroneDto> GetDroneById(int id)
        {
            var drone = await _unitOfWork.DroneRepository.FirstOrDefaultAsync(t => t.Id == id);
            return _mapper.Map<ReadDroneDto>(drone);
        }
        public async Task<ICollection<ReadMedicationsDto>>LoadDroneWithMedications(int droneId, List<Medication> medications)
        {
            var drone = await _unitOfWork.DroneRepository.FirstOrDefaultAsync(t => t.Id == droneId);
            drone.State = DroneState.LOADING;
            _unitOfWork.DroneRepository.Update(drone);
            await _unitOfWork.CommitAsync();
            
            drone.Medications = medications;
            drone.State = DroneState.LOADED;
            
            _unitOfWork.DroneRepository.Update(drone);
            await _unitOfWork.CommitAsync();
            
            return _mapper.Map<ICollection<ReadMedicationsDto>>(medications);
        }
        public async Task<ICollection<ReadMedicationsDto>>GetMedications(int droneId)
        {
            var medications = await _unitOfWork.MedicationRepository.FindAllAsync(t => t.DroneId == droneId);
            return _mapper.Map<ICollection<ReadMedicationsDto>>(medications);
        }
        public async Task<ICollection<ReadDroneDto>> GetAvailableDrones()
        {
            var availables = await _unitOfWork.DroneRepository
                .FindAllAsync(t => t.State == DroneState.IDLE && t.BatteryCapacity >= 25);

            return _mapper.Map<ICollection<ReadDroneDto>>(availables);
        }
        public async Task<List<Medication>> GetAllMedicationsFromIds(List<int> medicationsIds)
        {
            var found = await _unitOfWork.MedicationRepository.FindAllAsync(t => medicationsIds.Contains(t.Id));
            return found.ToList();
        }
    }
}
