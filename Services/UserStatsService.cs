using EficiaBackend.DTOs.Stats;
using EficiaBackend.Models;
using EficiaBackend.Repositories.Interfaces;
using EficiaBackend.Services.Interfaces;
using System.Linq;

namespace EficiaBackend.Services
{
    public class UserStatsService : IUserStatsService
    {
        private readonly IUserStatsRepository _userStatsRepository;

        private readonly ITaskRepository _taskRepository;

        public UserStatsService(IUserStatsRepository userStatsRepository,ITaskRepository taskRepository)
        {
            _userStatsRepository = userStatsRepository;
            _taskRepository = taskRepository;
        }

        public async Task<UserStatsDto> GetStatsAsync(int userId)
        {
            var stats = await _userStatsRepository.GetStatsByUserIdAsync(userId);

            if (stats is { })
            {
                return new UserStatsDto
                {
                    CurrentStreak = stats.CurrentStreak,
                    LastActivityDate = stats.LastActivityDate,
                    TotalHoursFocused = stats.TotalHoursFocused,
                    TasksCompletedCount = stats.TasksCompletedCount
                };
            }

            return new UserStatsDto
            {
                CurrentStreak = 0,
                LastActivityDate = DateTime.MinValue,
                TotalHoursFocused = 0,
                TasksCompletedCount = 0
            };
        }

        public async Task UpdateStatsCalculationsAsync(int userId)
        {
            // 3. OJO AQUÍ: Verifica si tu compañera llamó al método GetByIdAsync o GetTasksByUserIdAsync
            // GetById suele traer UNO solo. Tú necesitas la LISTA. 
            // Asumiré que es GetTasksByUserIdAsync por el contexto anterior.
            var allTasks = await _taskRepository.GetTasksByUserIdAsync(userId);
            // 4. CORREGIDO: .Where con Mayúscula
            var finishedTasks = allTasks.Where(t => t.Completed && t.CompletedAt.HasValue).ToList();

            // Calcular Estadísticas reales
            int totalTasks = finishedTasks.Count;
            int currentStreak = CalculateStreak(finishedTasks.Select(t => t.CompletedAt!.Value).ToList());

            // Buscar el registro actual en la BD
            var stats = await _userStatsRepository.GetStatsByUserIdAsync(userId);

            if (stats == null)
            {
                stats = new UserStats
                {
                    UserId = userId,
                    TasksCompletedCount = totalTasks,
                    CurrentStreak = currentStreak,
                    TotalHoursFocused = 0,
                    LastActivityDate = DateTime.UtcNow
                };
                await _userStatsRepository.AddStatsAsync(stats);
            }
            else
            {
                stats.TasksCompletedCount = totalTasks;
                stats.CurrentStreak = currentStreak;
                stats.LastActivityDate = DateTime.UtcNow;

                await _userStatsRepository.UpdateStatsAsync(stats);
            }
        }

        // Algoritmo para calcular racha
        private int CalculateStreak(List<DateTime> dates)
        {
            if (!dates.Any()) return 0;

            var orderedDates = dates.Select(d => d.Date).Distinct().OrderByDescending(d => d).ToList();
            var today = DateTime.UtcNow.Date;

            if (orderedDates.First() != today && orderedDates.First() != today.AddDays(-1))
            {
                return 0;
            }

            int streak = 0;
            var checkDate = orderedDates.First() == today ? today : today.AddDays(-1);

            foreach (var date in orderedDates)
            {
                if (date == checkDate)
                {
                    streak++;
                    checkDate = checkDate.AddDays(-1);
                }
                else
                {
                    break;
                }
            }
            return streak;
        }
    }
}