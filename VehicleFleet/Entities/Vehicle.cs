using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleFleet.Entities
{
    public class Vehicle
    {
        [MinLength(2)]
        public string Manufacturer { get; }
        
        [MinLength(2)]
        public string ModelName { get; }
        
        [Range(1970, 2021)]
        public int ProductionYear { get; init; } = DateTime.Now.Year;
        
        [Range(0, int.MaxValue)]
        public int EnginePowerHp { get; init; }

        [Range(0.0, double.MaxValue)]
        public double EngineVolumeLiters { get; init; }
        
        [Range(0, int.MaxValue)]
        public int InitialCostRoubles { get; }
        
        [Range(0, int.MaxValue)]
        public int CurrentCostRoubles { get; set; }
        
        public GearboxType GearboxType { get; init; }
        
        [Range(0.0, double.MaxValue)]
        public double Kilometrage { get; set; }

        [Range(0.0, double.MaxValue)]
        public double FuelConsumptionLitersPer100Km { get; init; }

        public string Name => $"{Manufacturer} {ModelName}";

        public Vehicle(string manufacturer, string modelName, int initialCostRoubles)
        {
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            ModelName = modelName ?? throw new ArgumentNullException(nameof(modelName));
            InitialCostRoubles = initialCostRoubles;
            CurrentCostRoubles = InitialCostRoubles;
        }
    }

    public enum GearboxType
    {
        Manual,
        Auto
    }
}