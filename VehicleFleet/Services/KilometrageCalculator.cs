using System.Linq;
using Microsoft.EntityFrameworkCore;
using VehicleFleet.Database;
using VehicleFleet.Entities;

namespace VehicleFleet.Services
{
    public class KilometrageCalculator
    {
        private readonly ApplicationDbContext _dbContext;

        public KilometrageCalculator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public double GetTotalKilometrageFor(Vehicle vehicle)
        {
            return _dbContext.Shifts.AsNoTracking()
                .Where(shift => shift.Vehicle.Id == vehicle.Id)
                .Sum(shift => shift.Kilometrage);
        }
    }
}