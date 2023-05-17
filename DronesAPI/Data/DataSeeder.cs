using DronesAPI.Models;

namespace DronesAPI.Data
{
    public class DataSeeder
    {
        private readonly DroneDBContext _dbContext;

        public DataSeeder(DroneDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
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
                new Drone { SerialNumber = "SN010", Model = DroneModel.Middleweight, WeightLimit = 500, BatteryCapacity = 2, State = DroneState.IDLE },
            };
                

                _dbContext.Drones.AddRange(drones);
                _dbContext.SaveChanges();
            }
            if (!_dbContext.Medications.Any())
            {
                var medications = new List<Medication>
                    {
                        new Medication { Name = "Medication 1", Weight = 150, Code = "ABC123", Image = new byte[0] },
                        new Medication { Name = "Medication 2", Weight = 145, Code = "DEF456", Image = new byte[0] },
                        new Medication { Name = "Medication 3", Weight = 400, Code = "XSFGG11", Image = new byte[0] },
                        new Medication { Name = "Medication 4", Weight = 500, Code = "FGVBDFD1", Image = new byte[0] },
                        new Medication { Name = "Medication 5", Weight = 200, Code = "ADFFFF", Image = new byte[0] },
                        new Medication { Name = "Medication 6", Weight = 10, Code = "XCVGGGG", Image = new byte[0] },
                        new Medication { Name = "Medication 7", Weight = 11, Code = "BVHBDDS12", Image = new byte[0] },
                        new Medication { Name = "Medication 8", Weight = 40, Code = "VBGFFD", Image = new byte[0] },
                    };
                _dbContext.Medications.AddRange(medications);
                _dbContext.SaveChanges();
            }
        }
    }
}
