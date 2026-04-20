using VineyardManagementSystem.Models;
using VineyardManagementSystem.Repositories;
using VineyardManagementSystem.Services;

public class PlotService : IPlotService
{
    private readonly IPlotRepository _plotRepo;
    private readonly IVineyardRepository _vineyardRepo;

    public PlotService(IPlotRepository plotRepo, IVineyardRepository vineyardRepo)
    {
        _plotRepo = plotRepo;
        _vineyardRepo = vineyardRepo;
    }

    public async Task CreatePlotAsync(Plot plot)
    {
        await ValidatePlotArea(plot);
        await _plotRepo.AddAsync(plot);
    }

    public async Task UpdatePlotAsync(Plot plot)
    {
        await ValidatePlotArea(plot);
        await _plotRepo.UpdateAsync(plot);
    }

    private async Task ValidatePlotArea(Plot plot)
    {
        var vineyard = await _vineyardRepo.GetByIdAsync(plot.VineyardId);
        if (vineyard == null) throw new Exception("Лозето не съществува.");

        // Взимаме всички парцели в това лозе
        var allPlots = await _plotRepo.GetAllAsync();
        var currentPlotsInVineyard = allPlots.Where(p => p.VineyardId == plot.VineyardId && p.Id != plot.Id);

        double occupiedArea = currentPlotsInVineyard.Sum(p => p.AreaSize);
        double remainingArea = vineyard.Size - occupiedArea;

        if (plot.AreaSize > remainingArea)
        {
            throw new Exception($"Няма достатъчно място! Свободна площ в '{vineyard.Name}': {remainingArea} дка.");
        }
    }

    public async Task<IEnumerable<Plot>> GetAllPlotsAsync() => await _plotRepo.GetAllAsync();
    public async Task<Plot?> GetPlotByIdAsync(int id) => await _plotRepo.GetByIdAsync(id);
    public async Task DeletePlotAsync(int id) => await _plotRepo.DeleteAsync(id);
}