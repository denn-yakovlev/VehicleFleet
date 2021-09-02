using VehicleFleet.Entities;

namespace VehicleFleet.Services
{
    public abstract class ExpenseCalculator : IExpenseCalculator
    {
        protected ExpenseContext _ctx;

        public ExpenseCalculator(ExpenseContext ctx)
        {
            _ctx = ctx;
        }

        public abstract double Calculate(Vehicle vehicle, int year);
    }
}