using DronesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Dtos
{
    public class ReadMedicationsDto
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Code { get; set; }
        public byte[] Image { get; set; }
    }
}
