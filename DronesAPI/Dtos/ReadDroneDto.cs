using DronesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Dtos
{
    public class ReadDroneDto
    {

        public int Id { get; set; }
        public string SerialNumber { get; set; }

        public DroneModel Model { get; set; }

        public double WeightLimit { get; set; }

        public int BatteryCapacity { get; set; }

        public DroneState State { get; set; }
    }
}
