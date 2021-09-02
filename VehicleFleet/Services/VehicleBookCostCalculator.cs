using System.Collections.Generic;
using System.Linq;
using VehicleFleet.Database;
using VehicleFleet.Entities;

namespace VehicleFleet.Services
{
    public class VehicleBookCostCalculator
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEnumerable<IExpenseCalculator> _expenseCalculators;

        public VehicleBookCostCalculator(
            ApplicationDbContext dbContext, 
            IEnumerable<IExpenseCalculator> expenseCalculators
        ) {
            _dbContext = dbContext;
            _expenseCalculators = expenseCalculators;
        }

        public IEnumerable<CostByYear> Calculate(Vehicle vehicle, int yearFromInclusive, int yearToInclusive)
        {
            for (int year = yearFromInclusive; year <= yearToInclusive; year++)
            {
                yield return new CostByYear
                {
                    Year = year,
                    Cost = Calculate(vehicle, year)
                };
            }
        }
        private double Calculate(Vehicle vehicle, int year)
        {
            return vehicle.InitialCostRoubles - _expenseCalculators.Sum(calc => calc.Calculate(vehicle, year));
        }
    }

    public struct CostByYear
    {
        public int Year { get; init; }
        public double Cost { get; init; }
    }
}