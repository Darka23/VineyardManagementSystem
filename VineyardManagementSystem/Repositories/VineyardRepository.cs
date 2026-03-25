using Microsoft.EntityFrameworkCore;
using VineyardManagementSystem.Data;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public class VineyardRepository : IVineyardRepository
    {
        private readonly ApplicationDbContext _context;

        public VineyardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vineyard vineyard)
        {
            await _context.Vineyards.AddAsync(vineyard);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var vineyard = await _context.Vineyards.FindAsync(id);
            if (vineyard != null)
            {
                _context.Vineyards.Remove(vineyard);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Vineyard>> GetAllAsync()
            => await _context.Vineyards.ToListAsync();


        public async Task<Vineyard> GetByIdAsync(int id)
            => await _context.Vineyards.FindAsync(id);

        public async Task UpdateAsync(Vineyard vineyard)
        {
            _context.Vineyards.Update(vineyard);
            await _context.SaveChangesAsync();
        }
    }
}
