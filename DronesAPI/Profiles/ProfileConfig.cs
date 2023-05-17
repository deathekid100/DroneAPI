using AutoMapper;
using DronesAPI.Dtos;
using DronesAPI.Models;

namespace DronesAPI.Config
{
    public class ProfileConfig : Profile
    {
        public ProfileConfig()
        {
            CreateMap<Drone, ReadDroneDto>();
            CreateMap<ReadDroneDto, Drone>();
            CreateMap<Drone, LoadedDroneDto>();
            CreateMap<CreateDroneDto, Drone>();
            CreateMap<Medication, ReadMedicationsDto>();
            CreateMap<ReadMedicationsDto, Medication>();
        }
    }
}
