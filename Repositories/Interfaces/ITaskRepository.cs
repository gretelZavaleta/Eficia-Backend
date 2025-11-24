using EficiaBackend.Models;

namespace EficiaBackend.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<TaskItem?> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(int userId);
        Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(int userId,bool isCompleted);
    }
}