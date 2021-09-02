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
    public class DriverController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public DriverController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var dtos = _dbContext.Drivers.Select(ToDriverDto);
            return Json(dtos.ToList());
        }

        private DriverDto ToDriverDto(Driver driver)
        {
            return _mapper.Map<DriverDto>(driver);
        }
    }
}