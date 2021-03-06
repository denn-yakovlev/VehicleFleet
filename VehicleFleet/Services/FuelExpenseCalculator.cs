using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VehicleFleet.Database;
using VehicleFleet.Entities;

namespace VehicleFleet.Services.FuelSpendingCalculator 
{
    public class FuelExpenseCalculator : ExpenseCalculator
    {
        private readonly ApplicationDbContext _dbContext;

        public FuelExpenseCalculator(ExpenseContext ctx, ApplicationDbContext dbContext) : base(ctx)
        {
            _dbContext = dbContext;
        }

        public override double Calculate(Vehicle vehicle, int year)
        {
            var yearBegin = new DateTime(year, 1, 1);
            var yearEnd = new DateTime(year, 12, 31);
            var shiftsInYear = _dbContext.Shifts.AsNoTracking()
                .Where(shift => shift.Start >= yearBegin && shift.Start <= yearEnd)
                .ToList();
            var fuelConsumedInYear = shiftsInYear.Sum(shift => shift.FuelConsumedLiters);
            return fuelConsumedInYear * _ctx.FuelPriceRoublesPerLiter;
        }
    }
}