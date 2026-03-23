using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Repositories
{
    public interface IVineyardRepository
    {
        Task<IEnumerable<Vineyard>> GetAllAsync();
        Task<Vineyard> GetByIdAsync(int id);
        Task AddAsync(Vineyard vineyard);
        Task UpdateAsync(Vineyard vineyard);
        Task DeleteAsync(int id);
    }
}
