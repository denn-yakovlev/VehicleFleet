using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleFleet.Database;
using VehicleFleet.DTO;
using VehicleFleet.Entities;
using VehicleFleet.Services;

namespace VehicleFleet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IKilometrageCalculator _kilometrageCalculator;
        private readonly VehicleBookCostCalculator _vehicleBookCostCalculator;

        public VehicleController(
            ApplicationDbContext dbContext, 
            IMapper mapper, 
            IKilometrageCalculator kilometrageCalculator, 
            VehicleBookCostCalculator vehicleBookCostCalculator
        ) {
            _dbContext = dbContext;
            _mapper = mapper;
            _kilometrageCalculator = kilometrageCalculator;
            _vehicleBookCostCalculator = vehicleBookCostCalculator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var dtos = _dbContext.Vehicles.Select(ToVehicleDto);
            return Json(dtos.ToList());
        }

        private VehicleDto ToVehicleDto(Vehicle vehicle)
        {
            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);
            vehicleDto.Kilometrage = _kilometrageCalculator.GetTotalKilometrageFor(vehicle);
            vehicleDto.BookCostByYears = _vehicleBookCostCalculator.Calculate(
                vehicle,
                DateTime.Now.AddYears(-5).Year,
                DateTime.Now.AddYears(-1).Year
            );
            return vehicleDto;
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var entity = new Vehicle {Id = id};
            _dbContext.Attach(entity);
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}