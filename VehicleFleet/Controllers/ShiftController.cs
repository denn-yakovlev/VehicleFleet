using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleFleet.Database;
using VehicleFleet.DTO;
using VehicleFleet.Entities;

namespace VehicleFleet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public ShiftController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var dtos = _dbContext.Shifts.Include(s => s.Driver).Include(s => s.Vehicle).Select(ToShiftDto);
            return Json(dtos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _dbContext.Shifts.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Shifts.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(ShiftDto shiftDto)
        {
            var entity = _mapper.Map<Shift>(shiftDto);
            _dbContext.Shifts.Add(entity);
            await _dbContext.SaveChangesAsync();
            return Created("api/[controller]", entity);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ShiftDto shiftDto)
        {
            var updatedEntity = _mapper.Map<Shift>(shiftDto);
            var entity = await _dbContext.Shifts.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbContext.Shifts.Add(updatedEntity);
            _dbContext.Entry(updatedEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        
        private ShiftDto ToShiftDto(Shift shift)
        {
            return _mapper.Map<ShiftDto>(shift);
        }
    }
}