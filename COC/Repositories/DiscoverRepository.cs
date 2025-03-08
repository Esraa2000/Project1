using COC.Models;
using COC.ModelDB.QUDB;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace COC.Repositories
{
    public class DiscoverRepository : IDiscoverRepository
    {
        private readonly QUDBContext _context;

        public DiscoverRepository(QUDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Discover>> GetAll()
        {
            return await _context.Discovers.ToListAsync();
        }


        public async Task<Discover> GetById(int id)
        {
            return await _context.Discovers.FindAsync(id);
        }
        public async Task Add(Discover discover)
        {
            await _context.Discovers.AddAsync(discover);
            await _context.SaveChangesAsync();
        }


        public async Task Update(Discover discover)
        {
            _context.Discovers.Update(discover);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _context.Discovers.FindAsync(id);
            if (item != null)
            {
                _context.Discovers.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

    }
    
}