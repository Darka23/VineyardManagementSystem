using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public interface IGrapeVarietyRepository
    {
        Task<IEnumerable<GrapeVariety>> GetAllAsync();
        Task AddAsync(GrapeVariety variety);
        Task<GrapeVariety?> GetByIdAsync(int id);
        Task UpdateAsync(GrapeVariety variety);
        Task DeleteAsync(int id);

    }
}
