using VineyardManagementSystem.Models;
using VineyardManagementSystem.Repositories;

namespace VineyardManagementSystem.Services
{
    public class GrapeVarietyService : IGrapeVarietyService
    {
        private readonly IGrapeVarietyRepository _repo;

        public GrapeVarietyService(IGrapeVarietyRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<GrapeVariety>> GetAllGrapeVarietiesAsync()
            => await _repo.GetAllAsync();

        public async Task<GrapeVariety?> GetGrapeVarietyByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task CreateGrapeVarietyAsync(GrapeVariety variety)
        {
            await ValidateVariety(variety);
            await _repo.AddAsync(variety);
        }

        public async Task UpdateGrapeVarietyAsync(GrapeVariety variety)
        {
            await ValidateVariety(variety);
            await _repo.UpdateAsync(variety);
        }

        public async Task DeleteGrapeVarietyAsync(int id)
        {
            var variety = await _repo.GetByIdAsync(id);
            if (variety == null)
            {
                throw new Exception("Сортът не е намерен.");
            }

            // ВАЖНО: Тук по-нататък ще спрем изтриването, ако сортът се ползва в парцел
            await _repo.DeleteAsync(id);
        }

        private async Task ValidateVariety(GrapeVariety variety)
        {
            if (string.IsNullOrWhiteSpace(variety.Name))
                throw new Exception("Името на сорта е задължително.");

            var all = await _repo.GetAllAsync();
            if (all.Any(v => v.Name.ToLower() == variety.Name.ToLower() && v.Id != variety.Id))
            {
                throw new Exception($"Сортът '{variety.Name}' вече съществува в базата!");
            }
        }
    }
}