using Microsoft.EntityFrameworkCore;
using SimpleCarDb.Data;
using SimpleCarDb.Interfaces;
using SimpleCarDb.Models;

namespace SimpleCarDb.Repositories
{
    public class EngineRepository : IEngineRepository
    {
        private readonly AppDbContext _context;

        public EngineRepository(AppDbContext context) //db csere esetén is működik elvileg
        {
            _context = context;
        }

        public async Task<IEnumerable<EngineDetail>> GetAll()
        {
            return await _context.EngineDetails.ToListAsync();
        }

        public async Task<EngineDetail?> Get(int id)
        {
            return await _context.EngineDetails.FindAsync(id);
        }

        public async Task<EngineDetail> Create(EngineDetail theEngine)
        {
            await _context.EngineDetails.AddAsync(theEngine);
            await _context.SaveChangesAsync();
            return theEngine;
        }

        public async Task<EngineDetail?> Update(int id, EngineDetail theEngine)
        {
            var existingEngine = await _context.EngineDetails.FindAsync(id);
            if (existingEngine == null)
            {
                return null;
            }
            existingEngine.EngineNumber = theEngine.EngineNumber;
            await _context.SaveChangesAsync();
            return theEngine;
        }

        public async Task<bool> Delete(int id)
        {
            var theChosenOne = await _context.EngineDetails.FindAsync(id);
            if (theChosenOne == null)
            {
                return false;
            }
            _context.EngineDetails.Remove(theChosenOne);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
