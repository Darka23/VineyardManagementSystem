using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Services
{
    public interface IHarvestService
    {
        Task<IEnumerable<Harvest>> GetAllHarvestsAsync();
        Task<Harvest?> GetByIdAsync(int id);
        Task CreateHarvestAsync(Harvest harvest);
        Task UpdateHarvestAsync(Harvest harvest);
        Task DeleteHarvestAsync(int id);
    }
}
