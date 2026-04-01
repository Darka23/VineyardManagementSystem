using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Services
{
    public interface IVineyardService
    {
        Task<IEnumerable<Vineyard>> GetVineyardsListAsync();

        public Task<Vineyard?> GetVineyardByIdAsync(int id);

        public Task CreateVineyardAsync(Vineyard vineyard);

        public Task UpdateVineyardAsync(Vineyard vineyard);

        public Task DeleteVineyardAsync(int id);

    }
}
