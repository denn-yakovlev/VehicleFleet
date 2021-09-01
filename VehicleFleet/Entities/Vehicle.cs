using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleFleet.Entities
{
    public class Vehicle
    {
        public int Id { get; init; }
        
        [MinLength(2)]
        public string Manufacturer { get; init; }
        
        [MinLength(2)]
        public string ModelName { get; init; }
        
        [Range(1970, 2021)]
        public int ProductionYear { get; init; }
        
        [Range(0, int.MaxValue)]
        public int EnginePowerHp { get; init; }

        [Range(0.0, double.MaxValue)]
        public double EngineVolumeLiters { get; init; }
        
        [Range(0, int.MaxValue)]
        public int InitialCostRoubles { get; init; }
        
        [Range(0, int.MaxValue)]
        public int CurrentCostRoubles { get; set; }
        
        public GearboxType GearboxType { get; init; }
        
        [Range(0.0, double.MaxValue)]
        public double Kilometrage { get; init; }

        [Range(0.0, double.MaxValue)]
        public double FuelConsumptionLitersPer100Km { get; set; }

        public string Name => $"{Manufacturer} {ModelName}";
    }

    public enum GearboxType
    {
        Manual,
        Auto
    }
}