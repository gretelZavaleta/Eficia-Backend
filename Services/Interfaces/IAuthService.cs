using EficiaBackend.DTOs.Auth;

namespace EficiaBackend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request); 
        Task<AuthResponse> LoginAsync(LoginRequest request); 
        Task<bool> UserExistsAsync(string email); //validar email unico
    }
}