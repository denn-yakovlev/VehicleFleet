using Microsoft.EntityFrameworkCore;
using Npgsql;
using VehicleFleet.Entities;

namespace VehicleFleet.Database
{
    public class VehicleFleetDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public VehicleFleetDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new NpgsqlConnection("Host=localhost;Database=vehicle_fleet;Username=postgres;Password=password");
            optionsBuilder.UseNpgsql(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().Property(v => v.GearboxType).HasConversion<string>();
        }
    }
}