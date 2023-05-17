using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Drone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string SerialNumber { get; set; }

        [Required]
        public DroneModel Model { get; set; }

        [Required]
        [Range(0, 500)]
        public double WeightLimit { get; set; }

        [Required]
        [Range(0, 100)]
        public int BatteryCapacity { get; set; }

        [Required]
        public DroneState State { get; set; }
        public List<Medication> Medications { get; set; }
        public List<DroneLog> Logs { get; set; }
    }
}
