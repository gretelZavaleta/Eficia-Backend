using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using EficiaBackend.Data;
using EficiaBackend.DTOs.Auth;
using EficiaBackend.Models;
using EficiaBackend.Services.Interfaces;

namespace EficiaBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

      //Registro de usuario
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Verificar si el email ya existe
            if (await UserExistsAsync(request.Email))
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "El correo ya está registrado."
                };
            }

            // Crear nuevo usuario
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            // Guardarlo en base de datos
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generar JWT
            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Success = true,
                Message = "Usuario registrado exitosamente.",
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        //Login de usuario
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            // Buscar usuario por email
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Credenciales incorrectas."
                };
            }

            // Verificar contraseña
            bool passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!passwordValid)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Credenciales incorrectas."
                };
            }

            // Credenciales correctas -> generar token
            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Success = true,
                Message = "Inicio de sesión exitoso.",
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        //verificar si el usuario existe por email
        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
