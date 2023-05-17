using DronesAPI.Models;

namespace DronesAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DroneDBContext _context;

        public UnitOfWork(DroneDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DroneRepository ??= new GenericRepository<Drone>(_context);
            MedicationRepository ??= new GenericRepository<Medication>(_context);
            DroneLogRepository ??= new GenericRepository<DroneLog>(_context);
        }

        public IGenericRepository<Drone> DroneRepository { get; set; }
        public IGenericRepository<Medication> MedicationRepository { get; set; }
        public IGenericRepository<DroneLog> DroneLogRepository { get; set; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
