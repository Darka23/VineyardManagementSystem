using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Services
{
    public interface IVineyardService
    {
        Task<IEnumerable<Vineyard>> GetVineyardsListAsync();

        public Task GetVineyardByIdAsync(int id);

        public Task CreateVineyardAsync(Vineyard vineyard);

        public Task UpdateVineyard(Vineyard vineyard);

        public Task DeleteVineyard(int id);

    }
}
