using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleFleet.Database;
using VehicleFleet.DTO;
using VehicleFleet.Entities;

namespace VehicleFleet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public ShiftController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var dtos = _dbContext.Shifts.Select(ToShiftDto);
            return Json(dtos.ToList());
        }

        private ShiftDto ToShiftDto(Shift shift)
        {
            return _mapper.Map<ShiftDto>(shift);
        }
    }
}