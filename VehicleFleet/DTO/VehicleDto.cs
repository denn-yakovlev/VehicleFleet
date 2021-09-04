using System.Collections.Generic;
using VehicleFleet.Entities;
using VehicleFleet.Services;

namespace VehicleFleet.DTO
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string ModelName { get; set; }
        public int ProductionYear { get; set; }
        public int EnginePowerHp { get; set; }
        public double EngineVolumeLiters { get; set; }
        public int InitialCostRoubles { get; set; }
        // public int CurrentCostRoubles { get; set; }
        public GearboxType GearboxType { get; set; }
        public double Kilometrage { get; set; }
        public double FuelConsumptionLitersPer100Km { get; set; }
        public string Name { get; set; }
        public IEnumerable<CostByYear> BookCostByYears { get; set; }
    }
}