using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCarDb.Data;
using SimpleCarDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCarDb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Brand>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _context.Brands.Include(b => b.Cars).ToListAsync();
            return Ok(brands);
        }

        [HttpGet("{id:int}")] //név aztán típus
        [ProducesResponseType(typeof(Brand), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var brand = await _context.Brands.Include(b => b.Cars).FirstOrDefaultAsync(b => b.Id == id);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Brand), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Brand brand)
        {
            if (brand == null)
            {
                return BadRequest();
            }

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = brand.Id }, brand);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] Brand updatedBrand)
        {
            if (id != updatedBrand.Id)
            {
                return BadRequest("Az URL-ben szereplő ID nem egyezik a küldött objektum ID-jával.");
            }
            var existingBrand = await _context.Brands.FindAsync(id);
            if (existingBrand == null)
            {
                return NotFound();
            }
            else
            {
                existingBrand.Name = updatedBrand.Name;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            else
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
                return Ok("Törölve");
            }
        }

        

    }
}
