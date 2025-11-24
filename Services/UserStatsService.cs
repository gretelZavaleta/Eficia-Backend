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

        // TODO: GRETEL DESCOMENTA ESTO PORFIS
        // private readonly ITaskRepository _taskRepository; 

        // Actualizar constructor cuando inyectes el repo de tareas
        public UserStatsService(IUserStatsRepository userStatsRepository /*, ITaskRepository taskRepository */)
        {
            _userStatsRepository = userStatsRepository;
            // _taskRepository = taskRepository;
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

            // 1. Obtener tareas completadas
            // var allTasks = await _taskRepository.GetTasksByUserIdAsync(userId);
            // var finishedTasks = allTasks.Where(t => t.Completed && t.CompletedAt.HasValue).ToList();

            // 2. Calcular Estadísticas reales
            // int totalTasks = finishedTasks.Count;
            // int currentStreak = CalculateStreak(finishedTasks.Select(t => t.CompletedAt!.Value).ToList());

            // --- DATOS MOCK (Para que compile mientras tanto) ---
            int totalTasks = 0;
            int currentStreak = 0;
            // ----------------------------------------------------

            // 3. Buscar el registro actual en la BD
            var stats = await _userStatsRepository.GetStatsByUserIdAsync(userId);

            if (stats == null)
            {
                // Si el usuario es nuevo y completó su primera tarea, creamos el registro
                stats = new UserStats
                {
                    UserId = userId,
                    TasksCompletedCount = totalTasks,
                    CurrentStreak = currentStreak,
                    TotalHoursFocused = 0, // Pendiente: definir lógica de horas
                    LastActivityDate = DateTime.UtcNow
                };
                // Usamos el método Add que agregamos al repo
                await _userStatsRepository.AddStatsAsync(stats);
            }
            else
            {
                // Si ya existe, solo actualizamos los contadores
                stats.TasksCompletedCount = totalTasks;
                stats.CurrentStreak = currentStreak;
                stats.LastActivityDate = DateTime.UtcNow;

                // Usamos el método Update que agregamos al repo
                await _userStatsRepository.UpdateStatsAsync(stats);
            }
        }

        // Algoritmo para calcular racha de días consecutivos
        private int CalculateStreak(List<DateTime> dates)
        {
            if (!dates.Any()) return 0;

            // Ordenamos fechas únicas de más reciente a más antigua
            var orderedDates = dates.Select(d => d.Date).Distinct().OrderByDescending(d => d).ToList();
            var today = DateTime.UtcNow.Date;

            // Verificamos si la racha está viva (hizo algo hoy o ayer)
            if (orderedDates.First() != today && orderedDates.First() != today.AddDays(-1))
            {
                return 0; // Racha rota
            }

            int streak = 0;
            // Empezamos a contar desde hoy (o ayer si no hizo nada hoy pero la racha sigue viva)
            var checkDate = orderedDates.First() == today ? today : today.AddDays(-1);

            foreach (var date in orderedDates)
            {
                if (date == checkDate)
                {
                    streak++;
                    checkDate = checkDate.AddDays(-1); // Retrocedemos un día para buscar continuidad
                }
                else
                {
                    break; // Se rompió la continuidad
                }
            }
            return streak;
        }
    }
}