using VehicleFleet.Entities;

namespace VehicleFleet.Services
{
    public interface IExpenseCalculator
    {
        double Calculate(Vehicle vehicle, int year);
    }
}