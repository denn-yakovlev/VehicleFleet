using VehicleFleet.Entities;

namespace VehicleFleet.Services
{
    public class InsuranceExpenseCalculator : ExpenseCalculator
    {
        public InsuranceExpenseCalculator(ExpenseContext ctx) : base(ctx)
        {
        }

        public override double Calculate(Vehicle vehicle, int year)
        {
            return _ctx.InsuranceCoefficient * vehicle.InitialCostRoubles * vehicle.YearsInUse(year);
        }
    }
}