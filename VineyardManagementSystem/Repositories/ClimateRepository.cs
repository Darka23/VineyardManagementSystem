using Microsoft.EntityFrameworkCore;
using VineyardManagementSystem.Data;
using VineyardManagementSystem.Models;
using VineyardManagementSystem.Repositories;

public class ClimateRepository : IClimateRepository
{
    private readonly ApplicationDbContext _context;

    public ClimateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ClimateLog>> GetAllAsync()
    {
        return await _context.ClimateLogs
            .Include(c => c.Vineyard)
            .AsNoTracking()
            .OrderByDescending(c => c.LogDate)
            .ToListAsync();
    }

    public async Task<ClimateLog?> GetByIdAsync(int id)
    {
        return await _context.ClimateLogs
            .Include(c => c.Vineyard)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(ClimateLog log)
    {
        await _context.ClimateLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ClimateLog log)
    {
        _context.ClimateLogs.Update(log);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var log = await _context.ClimateLogs.FindAsync(id);
        if (log != null)
        {
            _context.ClimateLogs.Remove(log);
            await _context.SaveChangesAsync();
        }
    }
}