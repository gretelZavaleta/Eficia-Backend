using EficiaBackend.Models;

namespace EficiaBackend.Repositories.Interfaces
{
    public interface IUserStatsRepository
    {
        Task<UserStats?> GetStatsByUserIdAsync(int userId);
        Task AddStatsAsync(UserStats stats);
        Task UpdateStatsAsync(UserStats stats);
    }
}
