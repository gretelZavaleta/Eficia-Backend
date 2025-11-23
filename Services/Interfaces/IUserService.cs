using EficiaBackend.Models;
using EficiaBackend.DTOs.Users;

namespace EficiaBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserDto dto);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
