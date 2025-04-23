namespace CaptoneProject.Services.AssignmentAPI.Models
{
    public class Streak
    {
        public int Id { get; set; }
        public string LearnerId { get; set; }
        public DateTime LastInteractionDate { get; set; }
        public int CurrentStreak { get; set; }
    }
}
