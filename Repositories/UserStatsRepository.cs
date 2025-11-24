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

        public async Task AddStatsAsync(UserStats stats)
        {
            await _context.UserStats.AddAsync(stats);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatsAsync(UserStats stats)
        {
            _context.UserStats.Update(stats);
            await _context.SaveChangesAsync();
        }
    }
}
