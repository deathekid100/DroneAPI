using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 500)]
        public double Weight { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public byte[] Image { get; set; }
        public int? DroneId { get; set; }
        public Drone? Drone { get; set; }
    }
}
