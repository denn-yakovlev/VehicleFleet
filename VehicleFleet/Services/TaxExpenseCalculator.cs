using VehicleFleet.Entities;

namespace VehicleFleet.Services
{
    public class TaxExpenseCalculator : IExpenseCalculator
    {
        public double Calculate(Vehicle vehicle, int year)
        {
            return vehicle.InitialCostRoubles * 0.01 - vehicle.EnginePowerHp * 0.5;
        }
    }
}