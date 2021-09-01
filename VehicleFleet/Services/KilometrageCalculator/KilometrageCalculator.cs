﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using VehicleFleet.Database;
using VehicleFleet.Entities;

namespace VehicleFleet.Services
{
    public class KilometrageCalculator : IKilometrageCalculator
    {
        private readonly VehicleFleetDbContext _dbContext;

        public KilometrageCalculator(VehicleFleetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public double GetTotalKilometrageFor(Vehicle vehicle)
        {
            return _dbContext.Shifts.AsNoTracking()
                .Where(shift => shift.Vehicle.Id == vehicle.Id)
                .Sum(shift => shift.Kilometrage);
        }
    }
}