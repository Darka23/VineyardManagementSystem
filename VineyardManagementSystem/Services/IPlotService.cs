using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Services
{
    public interface IPlotService
    {
        Task CreatePlotAsync(Plot plot);
        Task UpdatePlotAsync(Plot plot);
        Task<IEnumerable<Plot>> GetAllPlotsAsync();
        Task<Plot?> GetPlotByIdAsync(int id);
        Task DeletePlotAsync(int id);
    }
}
