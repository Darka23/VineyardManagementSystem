using Microsoft.EntityFrameworkCore;
using VineyardManagementSystem.Data;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public class GrapeVarietyRepository : IGrapeVarietyRepository
    {
        private readonly ApplicationDbContext _context;

        public GrapeVarietyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(GrapeVariety variety)
        {
            await _context.GrapeVarieties.AddAsync(variety);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var variety = await _context.GrapeVarieties.FindAsync(id);
            if (variety != null)
            {
                _context.GrapeVarieties.Remove(variety);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GrapeVariety>> GetAllAsync()
            => await _context.GrapeVarieties.ToListAsync();

        public async Task<GrapeVariety?> GetByIdAsync(int id)
            => await _context.GrapeVarieties.FindAsync(id);

        public async Task UpdateAsync(GrapeVariety variety)
        {
            _context.GrapeVarieties.Update(variety);
            await _context.SaveChangesAsync();
        }
    }
}
