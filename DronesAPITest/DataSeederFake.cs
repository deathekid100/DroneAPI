
using DronesAPI.Data;
using DronesAPI.Models;

namespace DronesAPITest
{
    public class DataSeederFake
    {
        private readonly DroneDBContext _dbContext;

        public DataSeederFake(DroneDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            if (_dbContext.Drones.Any())
            {
                foreach (var drone in _dbContext.Drones)
                {
                    _dbContext.Drones.Remove(drone);
                }
                _dbContext.SaveChanges();
            }
            if (!_dbContext.Drones.Any())
            {
                var drones = new List<Drone>
                {
                    new Drone { SerialNumber = "SN001", Model = DroneModel.Lightweight, WeightLimit = 500, BatteryCapacity = 85, State = DroneState.IDLE },
                    new Drone { SerialNumber = "SN002", Model = DroneModel.Middleweight, WeightLimit = 400, BatteryCapacity = 80, State = DroneState.IDLE },
                    new Drone { SerialNumber = "SN003", Model = DroneModel.Cruiserweight, WeightLimit = 300, BatteryCapacity = 40, State = DroneState.IDLE },
                    new Drone { SerialNumber = "SN004", Model = DroneModel.Heavyweight, WeightLimit = 200, BatteryCapacity = 10, State = DroneState.IDLE },
                    new Drone { SerialNumber = "SN005", Model = DroneModel.Lightweight, WeightLimit = 100, BatteryCapacity = 25, State = DroneState.IDLE },
                    new Drone { SerialNumber = "SN006", Model = DroneModel.Middleweight, WeightLimit = 400, BatteryCapacity = 20, State = DroneState.IDLE },
                    new Drone { SerialNumber = "SN007", Model = DroneModel.Cruiserweight, WeightLimit = 10, BatteryCapacity = 40, State = DroneState.IDLE },
                    new Drone { SerialNumber = "SN008", Model = DroneModel.Heavyweight, WeightLimit = 20, BatteryCapacity = 0, State = DroneState.IDLE },
                    new Drone { SerialNumber = "SN009", Model = DroneModel.Lightweight, WeightLimit = 50, BatteryCapacity = 1, State = DroneState.IDLE },
                    new Drone { SerialNumber = "SN010", Model = DroneModel.Middleweight, WeightLimit = 500, BatteryCapacity = 2, State = DroneState.LOADED },
                };
                _dbContext.Drones.AddRange(drones);
                _dbContext.SaveChanges();
            }
        }
    }
}
