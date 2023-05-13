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
    }
}
