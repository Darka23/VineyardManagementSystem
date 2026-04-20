using Microsoft.EntityFrameworkCore;
using VineyardManagementSystem.Data;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public class HarvestRepository : IHarvestRepository
    {
        private readonly ApplicationDbContext _context;
        public HarvestRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Harvest>> GetAllAsync()
        {
            return await _context.Harvests
                .Include(h => h.Plot)
                .ThenInclude(p => p.Vineyard)
                .Include(h => h.Plot.GrapeVariety)
                .AsNoTracking()
                .OrderByDescending(h => h.HarvestDate)
                .ToListAsync();
        }

        public async Task<Harvest?> GetByIdAsync(int id)
        {
            return await _context.Harvests
                .Include(h => h.Plot)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AddAsync(Harvest harvest)
        {
            await _context.Harvests.AddAsync(harvest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Harvest harvest)
        {
            _context.Harvests.Update(harvest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var harvest = await _context.Harvests.FindAsync(id);
            if (harvest != null)
            {
                _context.Harvests.Remove(harvest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
