using DronesAPI.Models;
using System;

namespace DronesAPI.Data
{
    public interface IUnitOfWork
    {
        IGenericRepository<Drone> DroneRepository { get; set; }
        IGenericRepository<Medication> MedicationRepository { get; set; }
        Task<int> CommitAsync();
    }
}
