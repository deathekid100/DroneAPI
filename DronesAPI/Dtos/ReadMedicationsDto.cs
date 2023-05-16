using DronesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Dtos
{
    public class ReadMedicationsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Code { get; set; }
        public byte[] Image { get; set; }
    }
}
