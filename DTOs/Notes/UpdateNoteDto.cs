using System.ComponentModel.DataAnnotations;

namespace EficiaBackend.Dtos.Notes;

public class UpdateNoteDto
{
    [MaxLength(100)]
    public string? Title { get; set; } // Nullable: si es null, no se actualiza

    public string? Content { get; set; } // Nullable

    public bool? IsArchived { get; set; } // Nullable
}