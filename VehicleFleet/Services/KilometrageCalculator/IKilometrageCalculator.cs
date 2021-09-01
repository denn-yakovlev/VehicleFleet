using VehicleFleet.Entities;

namespace VehicleFleet.Services
{
    public interface IKilometrageCalculator
    {
        double GetTotalKilometrageFor(Vehicle vehicle);
    }
}