using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCarDb.Data;
using SimpleCarDb.Models;

namespace SimpleCarDb.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EngineController : Controller
    {
        private readonly AppDbContext _context;

        public EngineController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get all engine")]
        [ProducesResponseType(typeof(List<EngineDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var engines = await _context.EngineDetails.ToListAsync();
            return Ok(engines);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(EngineDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var engine = await _context.EngineDetails.FirstOrDefaultAsync(c => c.Id == id);
            return engine == null ? NotFound() : Ok(engine);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(EngineDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] EngineDetail theEngine)
        {
            var existingEngine = await _context.EngineDetails.FindAsync(id);
            if (existingEngine == null)
            {
                return NotFound();
            }
            existingEngine.EngineNumber = theEngine.EngineNumber;
            existingEngine.CapacityCc = theEngine.CapacityCc;
            existingEngine.Horsepower = theEngine.Horsepower;
            await _context.SaveChangesAsync();
            return Ok(existingEngine);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EngineDetail), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EngineDetail engine)
        {
            if (engine == null)
            {
                return BadRequest("Érvénytelen motor");
            }
            await _context.EngineDetails.AddAsync(engine);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {id=engine.Id}, engine);
        }
    }
}
