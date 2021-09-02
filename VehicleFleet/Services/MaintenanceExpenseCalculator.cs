using VehicleFleet.Entities;

namespace VehicleFleet.Services
{
    public class MaintenanceExpenseCalculator : ExpenseCalculator
    {
        public MaintenanceExpenseCalculator(ExpenseContext ctx) : base(ctx)
        {
        }

        public override double Calculate(Vehicle vehicle, int year)
        {
            return _ctx.MaintenanceCoefficient * vehicle.InitialCostRoubles * vehicle.YearsInUse(year);
        }
    }
}