using VineyardManagementSystem.Models;
using VineyardManagementSystem.Repositories;

namespace VineyardManagementSystem.Services
{
    public class VineyardService : IVineyardService
    {
        private readonly IVineyardRepository _repo;

        public VineyardService(IVineyardRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateVineyardAsync(Vineyard vineyard)
        {
            await _repo.AddAsync(vineyard);
        }

        public async Task DeleteVineyard(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task GetVineyardByIdAsync(int id)
        {
            await (_repo.GetByIdAsync(id));
        }

        public async Task<IEnumerable<Vineyard>> GetVineyardsListAsync() 
            => await _repo.GetAllAsync();

        public async Task UpdateVineyard(Vineyard vineyard)
        {
            await _repo.UpdateAsync(vineyard);
        }
    }
}
