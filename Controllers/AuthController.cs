using Microsoft.AspNetCore.Mvc;
using EficiaBackend.Services.Interfaces;
using EficiaBackend.DTOs.Auth;

namespace EficiaBackend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new
            {
                message = result.Message,
                token = result.Token,
                user = new
                {
                    id = result.UserId,
                    name = result.Name,
                    email = result.Email
                }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(new
            {
                message = result.Message,
                token = result.Token,
                user = new
                {
                    id = result.UserId,
                    name = result.Name,
                    email = result.Email
                }
            });
        }
    }
}