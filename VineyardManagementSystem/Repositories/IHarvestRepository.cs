using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public interface IHarvestRepository
    {
        Task<IEnumerable<Harvest>> GetAllAsync();
        Task<Harvest?> GetByIdAsync(int id);
        Task AddAsync(Harvest harvest);
        Task UpdateAsync(Harvest harvest);
        Task DeleteAsync(int id);
    }
}
