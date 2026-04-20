using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public interface IFieldActivityRepository
    {
        Task<IEnumerable<FieldActivity>> GetAllAsync();
        Task<FieldActivity?> GetByIdAsync(int id);
        Task AddAsync(FieldActivity activity);
        Task UpdateAsync(FieldActivity activity);
        Task DeleteAsync(int id);
    }
}
