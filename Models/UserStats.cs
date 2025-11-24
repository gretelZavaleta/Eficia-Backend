using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EficiaBackend.Models;

public class UserStats
{
    [Key]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public User? User { get; set; }

    public int CurrentStreak { get; set; }
    public DateTime LastActivityDate { get; set; }
    public float TotalHoursFocused { get; set; }
    public int TasksCompletedCount { get; set; }
}