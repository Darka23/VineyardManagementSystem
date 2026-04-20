using VineyardManagementSystem.Models;
using VineyardManagementSystem.Repositories;

namespace VineyardManagementSystem.Services
{
    public class HarvestService : IHarvestService
    {
        private readonly IHarvestRepository _repo;
        public HarvestService(IHarvestRepository repo) => _repo = repo;

        public async Task<IEnumerable<Harvest>> GetAllHarvestsAsync() => await _repo.GetAllAsync();
        public async Task<Harvest?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateHarvestAsync(Harvest harvest)
        {
            Validate(harvest);
            await _repo.AddAsync(harvest);
        }

        public async Task UpdateHarvestAsync(Harvest harvest)
        {
            Validate(harvest);
            await _repo.UpdateAsync(harvest);
        }

        public async Task DeleteHarvestAsync(int id) => await _repo.DeleteAsync(id);

        private void Validate(Harvest harvest)
        {
            if (harvest.QuantityKG <= 0)
                throw new Exception("Количеството трябва да бъде положително число.");

            if (harvest.SugarContent < 5 || harvest.SugarContent > 40)
                throw new Exception("Захарността трябва да бъде в реалистични граници (5-40%).");

            if (harvest.HarvestDate > DateTime.Now)
                throw new Exception("Датата на гроздобера не може да бъде в бъдещето.");
        }
    }
}
