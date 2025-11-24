using EficiaBackend.DTOs.Stats;
using EficiaBackend.Repositories.Interfaces;
using EficiaBackend.Services.Interfaces;

namespace EficiaBackend.Services
{
    public class UserStatsService : IUserStatsService
    {
        private readonly IUserStatsRepository _userStatsRepository;
        public UserStatsService(IUserStatsRepository userStatsRepository)
        {
            this._userStatsRepository = userStatsRepository;
        }
        public async Task<UserStatsDto> GetStatsAsync(int userId)
        {
            var stats = await _userStatsRepository.GetStatsByUserIdAsync(userId);
            if(stats is { })
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
    }
}
