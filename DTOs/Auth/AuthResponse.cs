namespace EficiaBackend.DTOs.Auth;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // Mensaje de éxito o error
    public string Message { get; set; } = string.Empty;

    // Indica si la operación fue exitosa
    public bool Success { get; set; } = true;
}
