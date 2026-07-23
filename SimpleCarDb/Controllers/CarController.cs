using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCarDb.Data;
using SimpleCarDb.Models;

namespace SimpleCarDb.Controllers
{

    [ApiController] //Automatikus Validáció, Intelligens Adatfelismerés(json) nem feltétlenül kell neki a FromBody/FromRoute, Kötelező Routolás, minden metódusnak legyen útvonala 
    [Route("[controller]")] //lehetne [Route("api/[controller]")] ez is, a megjelenő cím "localhost:5000/Car"
    public class CarController : Controller
    {
        private readonly AppDbContext _context;

        public CarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Car>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _context.Cars.Include(c => c.EngineDetail).ToListAsync();
            return Ok(cars);
        }
        [HttpGet("brand/{brand}")]
        [ProducesResponseType(typeof(List<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCarsFromOneBrand([FromRoute] string brand)
        {
            var cars = await _context.Cars
                .Where(c => c.Brand.Name.ToLower() == brand.ToLower())
                .ToListAsync();
            if (cars.Count == 0)
            {
                return NotFound($"Nem találtunk egyetlen autót sem a(z) {brand} márkától.");
            }
            return Ok(cars);
        }

        [HttpGet("except-brand/{brand}")]
        [ProducesResponseType(typeof(List<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCarsExceptFromThisBrand([FromRoute] string brand)
        {
            var cars = await _context.Cars
                .Where(c => c.Brand.Name.ToLower() != brand.ToLower())
                .ToListAsync();
            if (cars.Count == 0)
            {
                return NotFound($"Nem találtunk más márkájú autót a(z) {brand} kivételével.");
            }
            return Ok(cars);
        }
        
        [HttpGet("id nélkül")]
        [ProducesResponseType(typeof(List<Car>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWithoutIds()
        {
            var carDetails = await _context.Cars
                .Select(c => new
                {
                    Márka = c.Brand.Name,
                    Név = c.Name,
                    Típus = c.Type,
                    Motorszám = c.EngineDetail != null ? c.EngineDetail.EngineNumber : "Nincs adat",
                    Köbcenti = c.EngineDetail == null ? 0: c.EngineDetail.CapacityCc,
                    Lóerő = c.EngineDetail != null ? c.EngineDetail.Horsepower : 0
                })
                .ToListAsync();
            return Ok(carDetails);
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
            existingCar.Name = carToUpdate.Name;
            existingCar.Type = carToUpdate.Type;
            existingCar.BrandId = carToUpdate.BrandId;
            await _context.SaveChangesAsync();
            return NoContent();
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

        [HttpPut("{carId:int}/connect-engine/{engineId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConnectEngineToCar([FromRoute] int carId, [FromRoute] int engineId)
        {
            var engine = await _context.EngineDetails.FindAsync(engineId);
            if (engine == null)
            {
                return NotFound("A megadott motor nem található.");
            }
            var carExists = await _context.Cars.AnyAsync(c => c.Id == carId);
            if (!carExists)
            {
                return NotFound("A megadott autó nem található.");
            }
            engine.CarId = carId;
            await _context.SaveChangesAsync();
            return Ok(new { message = $"A {engineId} motor sikeresen összekötve a {carId} autóval." });
        }


        /*
        //hibás endpointok
        [HttpGet("carsFromThisBrand/{brand}")]
        [ProducesResponseType(typeof(List<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCarsFromOneBand([FromRoute] string brand)
        {
            var cars = await _context.Cars.Where(c => c.Brand.Name.ToLower() == brand.ToLower())
                .ToListAsync();
            return cars.Any() ? NotFound() : Ok(cars);
        }

        [HttpGet("carsButNotThisBrand/{brand}")]
        [ProducesResponseType(typeof(List<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCarsExceptFromThisBrand([FromRoute] string brand)
        {
            var cars = await _context.Cars.Where(c => c.Brand.Name.ToLower() != brand.ToLower()).ToListAsync();
            return cars.Any() ? NotFound() : Ok(cars); 
        }
        */
    }
}
