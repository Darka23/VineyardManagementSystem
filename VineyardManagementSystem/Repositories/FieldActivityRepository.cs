using Microsoft.EntityFrameworkCore;
using VineyardManagementSystem.Data;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public class FieldActivityRepository : IFieldActivityRepository
    {
        private readonly ApplicationDbContext _context;
        public FieldActivityRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<FieldActivity>> GetAllAsync()
        {
            return await _context.FieldActivities
                .Include(a => a.Plot)
                .ThenInclude(p => p.Vineyard)
                .AsNoTracking()
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<FieldActivity?> GetByIdAsync(int id)
        {
            return await _context.FieldActivities
                .Include(a => a.Plot)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(FieldActivity activity)
        {
            await _context.FieldActivities.AddAsync(activity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FieldActivity activity)
        {
            _context.FieldActivities.Update(activity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var activity = await _context.FieldActivities.FindAsync(id);
            if (activity != null)
            {
                _context.FieldActivities.Remove(activity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
