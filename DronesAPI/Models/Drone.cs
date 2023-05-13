using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Drone
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Serial number is required")]
        [StringLength(100, ErrorMessage = "Serial number cannot exceed 100 characters")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public DroneModel Model { get; set; }

        [Required(ErrorMessage = "Weight limit is required")]
        [Range(0, 500, ErrorMessage = "Weight limit must be between 0 and 500 grams")]
        public double WeightLimit { get; set; }

        [Required(ErrorMessage = "Battery capacity is required")]
        [Range(0, 100, ErrorMessage = "Battery capacity must be between 0 and 100 percent")]
        public int BatteryCapacity { get; set; }

        [Required(ErrorMessage = "State is required")]
        public DroneState State { get; set; }
        public List<Medication> Medications { get; set; }
    }
}
