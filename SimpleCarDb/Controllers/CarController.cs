using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCarDb.Models;

namespace SimpleCarDb.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CarController : Controller
    {
        private readonly Data.AppDbContext _context;

        public CarController(Data.AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Car>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _context.Cars.ToListAsync();
            return Ok(cars);
        }

        [HttpGet("{brand}")]
        [ProducesResponseType(typeof(List<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCarsFromOneBand([FromRoute] string brand)
        {
            var cars = await _context.Cars.Where(c => c.Brand.Name.ToLower() == brand.ToLower())
                .ToListAsync();
            return cars == null ? NotFound() : Ok(cars);
        }


    }
}
