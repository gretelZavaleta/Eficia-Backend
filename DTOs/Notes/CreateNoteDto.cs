using System.ComponentModel.DataAnnotations;

namespace EficiaBackend.Dtos.Notes;

public class CreateNoteDto
{
    [Required]
    [MaxLength(100)] 
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    [Required]
    public int UserId { get; set; } // Por ahora lo pedimos manual, luego lo sacaremos del Token
}