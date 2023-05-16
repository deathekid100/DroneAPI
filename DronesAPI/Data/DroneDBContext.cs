using DronesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DronesAPI.Data
{
    public class DroneDBContext : DbContext
    {
        public DroneDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Drone> Drones { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<DroneLog> DroneLogs { get; set; }
    }
}
