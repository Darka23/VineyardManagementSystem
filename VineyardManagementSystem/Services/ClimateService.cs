using VineyardManagementSystem.Models;
using VineyardManagementSystem.Repositories;

namespace VineyardManagementSystem.Services
{
    public class ClimateService : IClimateService
    {
        private readonly IClimateRepository _repo;

        public ClimateService(IClimateRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ClimateLog>> GetAllLogsAsync() => await _repo.GetAllAsync();

        public async Task<ClimateLog?> GetLogByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateLogAsync(ClimateLog log)
        {
            ValidateLog(log);
            await _repo.AddAsync(log);
        }

        public async Task UpdateLogAsync(ClimateLog log)
        {
            ValidateLog(log);
            await _repo.UpdateAsync(log);
        }

        public async Task DeleteLogAsync(int id) => await _repo.DeleteAsync(id);

        private void ValidateLog(ClimateLog log)
        {
            if (log.LogDate > DateTime.Now)
                throw new Exception("Датата на измерване не може да бъде в бъдещето.");

            if (log.Temperature < -40 || log.Temperature > 55)
                throw new Exception("Температурата трябва да бъде между -40 и +55 градуса.");

            if (log.Humidity < 0 || log.Humidity > 100)
                throw new Exception("Влажността трябва да бъде между 0% и 100%.");

            if (log.Rainfall < 0)
                throw new Exception("Валежите не могат да бъдат отрицателно число.");
        }
    }
}
