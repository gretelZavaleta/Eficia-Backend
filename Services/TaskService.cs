using EficiaBackend.Dtos.Tasks;
using EficiaBackend.Models;
using EficiaBackend.Repositories.Interfaces;
using EficiaBackend.Services.Interfaces;

namespace EficiaBackend.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return task == null ? null : MapToDto(task);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(MapToDto);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = await _taskRepository.GetByUserIdAsync(userId);
            return tasks.Select(MapToDto);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto, int userId)
        {
            var task = new TaskItem
            {
                UserId = userId,
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                DueDate = createTaskDto.DueDate,
                Priority = createTaskDto.Priority,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdTask = await _taskRepository.CreateAsync(task);
            return MapToDto(createdTask);
        }

        public async Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null) return null;

            existingTask.Title = updateTaskDto.Title;
            existingTask.Description = updateTaskDto.Description;
            existingTask.DueDate = updateTaskDto.DueDate;
            existingTask.Priority = updateTaskDto.Priority;
            existingTask.UpdatedAt = DateTime.UtcNow;

            // Manejar el estado de completado
            if (updateTaskDto.Completed && !existingTask.Completed)
            {
                existingTask.Completed = true;
                existingTask.CompletedAt = DateTime.UtcNow;
            }
            else if (!updateTaskDto.Completed && existingTask.Completed)
            {
                existingTask.Completed = false;
                existingTask.CompletedAt = null;
            }

            var updatedTask = await _taskRepository.UpdateAsync(existingTask);
            return updatedTask == null ? null : MapToDto(updatedTask);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }

        public async Task<TaskDto?> MarkAsCompletedAsync(int id, bool completed)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null) return null;

            existingTask.Completed = completed;
            existingTask.CompletedAt = completed ? DateTime.UtcNow : null;
            existingTask.UpdatedAt = DateTime.UtcNow;

            var updatedTask = await _taskRepository.UpdateAsync(existingTask);
            return updatedTask == null ? null : MapToDto(updatedTask);
        }

        private static TaskDto MapToDto(TaskItem task)
        {
            return new TaskDto
            {
                Id = task.Id,
                UserId = task.UserId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Completed = task.Completed,
                CompletedAt = task.CompletedAt,
                Priority = task.Priority,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
    }
}