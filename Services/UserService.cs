using EficiaBackend.Models;
using EficiaBackend.DTOs.Users;
using EficiaBackend.Repositories.Interfaces;
using EficiaBackend.Services.Interfaces;

namespace EficiaBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(CreateUserDto dto)
        {
            // Validación: verificar si el correo ya existe
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new System.Exception("El correo ya está registrado.");
            }

            // Transformar DTO a entidad User
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = System.DateTime.UtcNow
            };

            // Guardar en la base de datos
            return await _userRepository.CreateAsync(user);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
