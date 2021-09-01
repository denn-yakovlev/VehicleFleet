using System;

namespace VehicleFleet.Entities
{
    public class Shift
    {
        public DateTime Start { get; init; } = DateTime.Now.AddDays(-1.0);
        public DateTime End { get; init; } = DateTime.Now;
        public Vehicle Vehicle { get; }
        public Driver Driver { get; }
        public double Kilometrage { get; init; }
        public double FuelConsumedLiters => Vehicle.FuelConsumptionLitersPer100Km / 100.0 * Kilometrage;

        public Shift(Vehicle vehicle, Driver driver)
        {
            Vehicle = vehicle;
            Driver = driver;
        }
    }
}