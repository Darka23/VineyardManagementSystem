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

        public async Task<IEnumerable<Vineyard>> GetVineyardsListAsync()
            => await _repo.GetAllAsync();

        public async Task<Vineyard?> GetVineyardByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task CreateVineyardAsync(Vineyard vineyard)
        {
            ValidateVineyard(vineyard);
            await _repo.AddAsync(vineyard);
        }

        public async Task UpdateVineyardAsync(Vineyard vineyard)
        {
            ValidateVineyard(vineyard);
            await _repo.UpdateAsync(vineyard);
        }

        public async Task DeleteVineyardAsync(int id)
        {
            var vineyard = await _repo.GetByIdAsync(id);
            if (vineyard == null) throw new Exception("Лозето не е намерено.");

            // ТУК: След като направим PlotRepo, ще добавим проверка:
            // if (vineyard.Plots.Any()) throw new Exception("Не може да изтриете лозе с активни парцели!");

            await _repo.DeleteAsync(id);
        }

        private void ValidateVineyard(Vineyard vineyard)
        {
            if (vineyard.PlantingDate > DateTime.Now)
                throw new Exception("Датата на засаждане не може да бъде в бъдещето!");

            if (vineyard.Size < 0.1 || vineyard.Size > 5000)
                throw new Exception("Площта на масива трябва да е между 0.1 и 5000 дка.");

            if (string.IsNullOrWhiteSpace(vineyard.Name))
                throw new Exception("Името на масива е задължително.");
        }
    }
}