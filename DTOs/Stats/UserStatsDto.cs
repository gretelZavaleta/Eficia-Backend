using System;

namespace EficiaBackend.DTOs.Stats
{
    public class UserStatsDto
    {
        public int CurrentStreak {get; set; }
        public DateTime LastActivityDate {get; set; }
        public double TotalHoursFocused {get; set; }
        public int TasksCompletedCount {get; set; }
    }
}
