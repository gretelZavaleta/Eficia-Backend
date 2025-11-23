using EficiaBackend.Dtos.Auth; 
using EficiaBackend.Models; 

namespace EficiaBackend.Services.Interfaces;
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}