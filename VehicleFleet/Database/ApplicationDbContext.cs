using System;
using Microsoft.EntityFrameworkCore;
using VehicleFleet.Controllers;
using VehicleFleet.Entities;

namespace VehicleFleet.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().Property(v => v.GearboxType).HasConversion<string>();
            // var vehicles = new[]
            // {
            //     new Vehicle()
            //     {
            //         Id = -1,
            //         Manufacturer = "Audi",
            //         ModelName = "A5",
            //         ProductionYear = 2003,
            //         EnginePowerHp = 200,
            //         EngineVolumeLiters = 2.5,
            //         InitialCostRoubles = 2500_000,
            //         GearboxType = GearboxType.Auto,
            //         FuelConsumptionLitersPer100Km = 9.5
            //     }
            // };
            // var drivers = new[]
            // {
            //     new Driver()
            //     {
            //         Id = -2,
            //         FullName = "Жмышенко В.А.",
            //     },
            //     new Driver()
            //     {
            //         Id = -3,
            //         FullName = "Зубенко М.П.",
            //     },
            // };
            // var shifts = new[]
            // {
            //     new Shift()
            //     {
            //         Id = -4,
            //         Start = new DateTime(2021, 1, 1),
            //         End = new DateTime(2021, 1, 4),
            //         Kilometrage = 120.5,
            //         Driver = drivers[1],
            //         Vehicle = vehicles[0]
            //     }
            // };
            // modelBuilder.Entity<Vehicle>().HasData(vehicles);
            // modelBuilder.Entity<Driver>().HasData(drivers);
            // modelBuilder.Entity<Shift>().HasData(shifts);
        }
    }
}