using System.ComponentModel.DataAnnotations;
using EficiaBackend.Models;

namespace EficiaBackend.Dtos.Tasks;

public class UpdateTaskDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public bool Completed { get; set; }
    public TaskPriority Priority { get; set; }
}
