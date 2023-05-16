using DronesAPI.Models;
using System;

namespace DronesAPI.Data
{
    public interface IUnitOfWork
    {
        IGenericRepository<Drone> DroneRepository { get; set; }
        IGenericRepository<Medication> MedicationRepository { get; set; }
        IGenericRepository<DroneLog> DroneLogRepository { get; set; }
        Task<int> CommitAsync();
    }
}
