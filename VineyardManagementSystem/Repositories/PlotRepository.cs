using Microsoft.EntityFrameworkCore;
using VineyardManagementSystem.Data;
using VineyardManagementSystem.Models;
using VineyardManagementSystem.Repositories;

public class PlotRepository : IPlotRepository
{
    private readonly ApplicationDbContext _context;

    public PlotRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Plot>> GetAllAsync()
    {
        return await _context.Plots
            .Include(p => p.Vineyard)       // Вземи данните за лозето
            .Include(p => p.GrapeVariety)   // Вземи данните за сорта
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Plot?> GetByIdAsync(int id)
    {
        return await _context.Plots
            .Include(p => p.Vineyard)
            .Include(p => p.GrapeVariety)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Plot plot)
    {
        await _context.Plots.AddAsync(plot);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Plot plot)
    {
        _context.Plots.Update(plot);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var plot = await GetByIdAsync(id);
        if (plot != null)
        {
            _context.Plots.Remove(plot);
            await _context.SaveChangesAsync();
        }
    }
}