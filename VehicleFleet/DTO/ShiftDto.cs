using System;
using System.ComponentModel.DataAnnotations;
using VehicleFleet.Entities;

namespace VehicleFleet.DTO
{
    public class ShiftDto
    {
        public int Id { get; init; }
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
        public string VehicleName { get; init; }
        public string DriverFullName { get; init; }
        public double Kilometrage { get; init; }
        public double FuelConsumedLiters { get; init; }
    }
}