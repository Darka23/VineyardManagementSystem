using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Services
{
    public interface IFieldActivityService
    {
        Task<IEnumerable<FieldActivity>> GetAllActivitiesAsync();
        Task<FieldActivity?> GetByIdAsync(int id);
        Task CreateActivityAsync(FieldActivity activity);
        Task UpdateActivityAsync(FieldActivity activity);
        Task DeleteActivityAsync(int id);
    }
}
