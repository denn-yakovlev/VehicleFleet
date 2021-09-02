namespace VehicleFleet.Services
{
    public class ExpenseContext
    {
        public double DepreciationCoefficient { get; init; }
        public double MaintenanceCoefficient { get; init; }
        public double InsuranceCoefficient { get; init; }
        
        public double FuelPriceRoublesPerLiter { get; init; }
    }
}