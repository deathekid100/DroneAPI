using DronesAPI.Data;
using DronesAPI.Models;

namespace DronesAPI.Services
{
    public class AuditService : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task DroneBatteryLevelsReport()
        {
            var drones = _unitOfWork.DroneRepository.GetAll();
            foreach (var drone in drones)
            {
                var timestamp = DateTime.UtcNow;
                var log = new DroneLog()
                {
                    DroneId = drone.Id,
                    EventType = "BatteryLevel",
                    EventData = $"Battery level is: {drone.BatteryCapacity}",
                    Timestamp = timestamp
                };
                await _unitOfWork.DroneLogRepository.AddAsync(log);
            }
           await _unitOfWork.CommitAsync();
        }
    }
}
