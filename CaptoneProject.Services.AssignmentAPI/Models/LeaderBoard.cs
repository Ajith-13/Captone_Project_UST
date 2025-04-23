using System.ComponentModel.DataAnnotations;

namespace CaptoneProject.Services.AssignmentAPI.Models
{
    public class LeaderBoard
    {
        public int Id { get; set; }
        public string LearnerId { get; set; }
        public int TotalMarks { get; set; }
        public int StreakPoints { get; set; }
        public int AssignmentMarks { get; set; }
        public int Rank { get; set; }
    }
}
