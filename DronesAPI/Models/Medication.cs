using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Name can only contain letters, numbers, hyphens, and underscores")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(0, 500, ErrorMessage = "Weight must be between 0 and 500 grams")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [RegularExpression(@"^[A-Z0-9_]+$", ErrorMessage = "Code can only contain upper case letters, numbers, and underscores")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public byte[] Image { get; set; }
        public int? DroneId { get; set; }
        public Drone? Drone { get; set; }
    }
}
