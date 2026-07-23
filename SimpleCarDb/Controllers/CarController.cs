using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCarDb.Data;
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            return car == null ? NotFound() : Ok(car);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] Car carToUpdate)
        {
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null)
            {
                return NotFound();
            }
            else
            {
                existingCar.Name = carToUpdate.Name;
                existingCar.Type = carToUpdate.Type;
                existingCar.BrandId = carToUpdate.BrandId;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            else
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return Ok("Törölve");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Car), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Car car)
        {
            if (car == null)
            {
                return BadRequest("Érvénytelen/üres autó");
            }
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = car.Id }, car);
        }

    }
}
