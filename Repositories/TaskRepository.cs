using Microsoft.EntityFrameworkCore;
using EficiaBackend.Data;
using EficiaBackend.Models;
using EficiaBackend.Repositories.Interfaces;

namespace EficiaBackend.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId)
        {
            return await _context.Tasks
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateAsync(TaskItem task)
        {
            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null) return null;

            _context.Entry(existingTask).CurrentValues.SetValues(task);
            existingTask.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return existingTask;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks
                                 .Where(t => t.UserId == userId) // Filtra por usuario
                                 .ToListAsync();
        }
        public async Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(int userId,bool isCompleted)
        {
            var query = _context.Tasks.Where(t => t.UserId == userId && t.Completed == isCompleted);

            // Si es Historial (Completadas), ordenamos por fecha de término (lo más nuevo arriba)
            if (isCompleted)
                return await query.OrderByDescending(t => t.CompletedAt).ToListAsync();

            // Si es Pendiente, ordenamos por urgencia (DueDate) o creación
            return await query.OrderBy(t => t.DueDate).ToListAsync();
        }
    }
}