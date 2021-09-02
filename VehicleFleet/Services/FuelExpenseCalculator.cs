using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VehicleFleet.Database;
using VehicleFleet.Entities;

namespace VehicleFleet.Services.FuelSpendingCalculator 
{
    public class FuelExpenseCalculator : ExpenseCalculator
    {
        private readonly VehicleFleetDbContext _dbContext;

        public FuelExpenseCalculator(ExpenseContext ctx, VehicleFleetDbContext dbContext) : base(ctx)
        {
            _dbContext = dbContext;
        }

        public override double Calculate(Vehicle vehicle, int year)
        {
            var yearBegin = new DateTime(year, 1, 1);
            var yearEnd = new DateTime(year, 12, 31);
            var fuelConsumedInYear = _dbContext.Shifts.AsNoTracking()
                .Where(shift => shift.Start >= yearBegin && shift.Start <= yearEnd)
                .Sum(shift => shift.FuelConsumedLiters);
            return fuelConsumedInYear * _ctx.FuelPriceRoublesPerLiter;
        }
    }
}