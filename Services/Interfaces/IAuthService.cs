using System.Threading.Tasks;
using EficiaBackend.DTOs.Auth;
using EficiaBackend.Models;

namespace EficiaBackend.Services.Interfaces;
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request); 
        Task<AuthResponse> LoginAsync(LoginRequest request); 
        Task<bool> UserExistsAsync(string email); //validar email unico
    }
}