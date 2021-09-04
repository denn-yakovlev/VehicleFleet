using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleFleet.Database;
using VehicleFleet.DTO;
using VehicleFleet.Entities;
using VehicleFleet.Services;

namespace VehicleFleet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Index()
        {
            var vehicles = await _dbContext.Vehicles.ToListAsync();
            var dtos = vehicles.Select(ToVehicleDto);
            return Json(dtos.ToList());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _dbContext.Vehicles.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Vehicles.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(VehicleDto shiftDto)
        {
            var entity = _mapper.Map<Vehicle>(shiftDto);
            _dbContext.Vehicles.Add(entity);
            await _dbContext.SaveChangesAsync();
            return Created("api/[controller]", entity);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VehicleDto shiftDto)
        {
            var updatedEntity = _mapper.Map<Vehicle>(shiftDto);
            var entity = await _dbContext.Vehicles.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbContext.Vehicles.Add(updatedEntity);
            _dbContext.Entry(updatedEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
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
    }
}