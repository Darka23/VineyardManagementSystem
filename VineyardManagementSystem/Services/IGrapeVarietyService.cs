using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Services
{
    public interface IGrapeVarietyService
    {
        Task<IEnumerable<GrapeVariety>> GetAllGrapeVarietiesAsync();
        Task CreateGrapeVarietyAsync(GrapeVariety variety);
        Task<GrapeVariety?> GetGrapeVarietyByIdAsync(int id);
        Task UpdateGrapeVarietyAsync(GrapeVariety variety);
        Task DeleteGrapeVarietyAsync(int id);

    }
}
