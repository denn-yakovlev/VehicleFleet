using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using VehicleFleet.Database;
using VehicleFleet.DTO;
using VehicleFleet.Entities;

namespace VehicleFleet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public DriverController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var dtos = _dbContext.Drivers.Select(ToDriverDto);
            return Json(dtos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _dbContext.Drivers.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Drivers.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DriverDto driverDto)
        {
            var entity = _mapper.Map<Driver>(driverDto);
            _dbContext.Drivers.Add(entity);
            await _dbContext.SaveChangesAsync();
            return Created("api/[controller]", entity);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DriverDto driverDto)
        {
            var updatedEntity = _mapper.Map<Driver>(driverDto);
            var entity = await _dbContext.Drivers.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbContext.Drivers.Add(updatedEntity);
            _dbContext.Entry(updatedEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        
        private DriverDto ToDriverDto(Driver driver)
        {
            return _mapper.Map<DriverDto>(driver);
        }
    }
}