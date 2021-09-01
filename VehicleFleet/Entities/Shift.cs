using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleFleet.Entities
{
    public class Shift
    {
        public int Id { get; init; }
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
        public Vehicle Vehicle { get; init; }
        public Driver Driver { get; init; }

        [Range(0.0, double.MaxValue)] 
        public double Kilometrage { get; init; }

        public double FuelConsumedLiters => Vehicle.FuelConsumptionLitersPer100Km / 100.0 * Kilometrage;
    }
}