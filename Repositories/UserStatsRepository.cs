using EficiaBackend.Data;
using EficiaBackend.Models;
using EficiaBackend.Repositories.Interfaces;

namespace EficiaBackend.Repositories
{
    public class UserStatsRepository : IUserStatsRepository
    {
        private readonly AppDbContext _context;
        public UserStatsRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<UserStats?> GetStatsByUserIdAsync(int userId)
        {
            return await _context.UserStats.FindAsync(userId);
        }
    }
}
