using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Services
{
    public interface IClimateService
    {
        Task<IEnumerable<ClimateLog>> GetAllLogsAsync();
        Task<ClimateLog?> GetLogByIdAsync(int id);
        Task CreateLogAsync(ClimateLog log);
        Task UpdateLogAsync(ClimateLog log);
        Task DeleteLogAsync(int id);
    }
}
