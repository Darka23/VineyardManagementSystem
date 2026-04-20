using VineyardManagementSystem.Models;
using VineyardManagementSystem.Repositories;

namespace VineyardManagementSystem.Services
{
    public class FieldActivityService : IFieldActivityService
    {
        private readonly IFieldActivityRepository _repo;
        public FieldActivityService(IFieldActivityRepository repo) => _repo = repo;

        public async Task<IEnumerable<FieldActivity>> GetAllActivitiesAsync() => await _repo.GetAllAsync();
        public async Task<FieldActivity?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateActivityAsync(FieldActivity activity)
        {
            Validate(activity);
            await _repo.AddAsync(activity);
        }

        public async Task UpdateActivityAsync(FieldActivity activity)
        {
            Validate(activity);
            await _repo.UpdateAsync(activity);
        }

        public async Task DeleteActivityAsync(int id) => await _repo.DeleteAsync(id);

        private void Validate(FieldActivity activity)
        {
            if (activity.Date > DateTime.Now)
                throw new Exception("Не може да записвате дейност с бъдеща дата.");

            if (activity.Cost < 0)
                throw new Exception("Разходът не може да бъде отрицателно число.");

            if (string.IsNullOrWhiteSpace(activity.Description))
                throw new Exception("Описанието е задължително.");
        }
    }
}
