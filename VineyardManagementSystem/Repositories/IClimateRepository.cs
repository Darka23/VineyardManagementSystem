using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public interface IClimateRepository
    {
        Task<IEnumerable<ClimateLog>> GetAllAsync();
        Task<ClimateLog?> GetByIdAsync(int id);
        Task AddAsync(ClimateLog log);
        Task UpdateAsync(ClimateLog log);
        Task DeleteAsync(int id);
    }
}
