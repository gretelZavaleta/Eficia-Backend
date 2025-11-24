namespace EficiaBackend.Dtos.Notes;

public class NoteDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsArchived { get; set; }
    public DateTime CreatedAt { get; set; }
    // No devolvemos el UserId a menos que sea necesario explícitamente
}