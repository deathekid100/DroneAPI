using DronesAPI.Models;

namespace DronesAPI.Dtos
{
    public class CreateDroneDto
    {

        public string SerialNumber { get; set; }

        public DroneModel? Model { get; set; }

        public double WeightLimit { get; set; }

        public int BatteryCapacity { get; set; }

    }
}
