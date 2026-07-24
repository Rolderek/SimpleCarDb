using Microsoft.AspNetCore.Mvc;
using SimpleCarDb.Interfaces;
using SimpleCarDb.Models;

namespace SimpleCarDb.Controllers
{
    
    public class EngineRepositoryController : Controller
    {
        public readonly IEngineRepository _repo;

        public EngineRepositoryController(IEngineRepository repository)
        {
            _repo = repository;
        }

        [HttpGet("pero")]
        public async Task<ActionResult<IEnumerable<EngineDetail>>> GetAll()
        {
            var engines = await _repo.GetAll();
            return Ok(engines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EngineDetail>> Get(int id)
        {
            var engine = await _repo.Get(id);
            if (engine == null)
            {
                return NotFound();
            }
            return Ok(engine);
        }
    }
}
