using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public interface IPlotRepository
    {
        Task<IEnumerable<Plot>> GetAllAsync();
        Task<Plot?> GetByIdAsync(int id);
        Task AddAsync(Plot plot);
        Task UpdateAsync(Plot plot);
        Task DeleteAsync(int id);
    }
}
