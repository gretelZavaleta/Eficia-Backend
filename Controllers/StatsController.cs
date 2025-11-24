using EficiaBackend.DTOs.Stats;
using EficiaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EficiaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IUserStatsService _userStatsService;
        public StatsController(IUserStatsService userStatsService)
        {
            this._userStatsService = userStatsService;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserStatsDto>> GetUserStats(int userId)
        {
            var stats = await _userStatsService.GetStatsAsync(userId);
            return Ok(stats);
        }
    }
}
