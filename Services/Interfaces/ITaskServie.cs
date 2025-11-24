using EficiaBackend.Dtos.Tasks;

namespace EficiaBackend.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto?> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int userId);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto, int userId);
        Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto);
        Task<bool> DeleteTaskAsync(int id);
        Task<TaskDto?> MarkAsCompletedAsync(int id, bool completed);
        Task<IEnumerable<TaskDto>> GetPendingTasksAsync(int userId);
        Task<IEnumerable<TaskDto>> GetHistoryTasksAsync(int userId);
    }
}