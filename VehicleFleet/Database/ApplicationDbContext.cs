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
        }
    }
}