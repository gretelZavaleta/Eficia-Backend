using EficiaBackend.Models;

namespace EficiaBackend.Repositories.Interfaces
{
    public interface IUserStatsRepository
    {
        Task<UserStats?> GetStatsByUserIdAsync(int userId);
    }
}
