using VehicleFleet.Entities;

namespace VehicleFleet.Services
{   
    public class DepreciationExpenseCalculator : ExpenseCalculator
    {
        public DepreciationExpenseCalculator(ExpenseContext ctx) : base(ctx)
        {
        }

        public override double Calculate(Vehicle vehicle, int year)
        {
            return _ctx.DepreciationCoefficient * vehicle.InitialCostRoubles * vehicle.YearsInUse(year);
        }
    }
}