﻿using DronesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Dtos
{
    public class LoadedDroneDto
    {

        public string SerialNumber { get; set; }

        public DroneModel Model { get; set; }

        public double WeightLimit { get; set; }

        public int BatteryCapacity { get; set; }

        public DroneState State { get; set; }

        public List<Medication> Medications { get; set; }
    }
}
