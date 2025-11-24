using EficiaBackend.DTOs.Stats;

namespace EficiaBackend.Services.Interfaces
{
    public interface IUserStatsService
    {
        Task<UserStatsDto> GetStatsAsync(int userId);
        Task UpdateStatsCalculationsAsync(int userId); 
    }
}
