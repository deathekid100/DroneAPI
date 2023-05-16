using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class DroneLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EventType { get; set; }
        [Required]
        public string EventData { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public int DroneId { get; set; }
        public Drone Drone { get; set; }

    }
}
