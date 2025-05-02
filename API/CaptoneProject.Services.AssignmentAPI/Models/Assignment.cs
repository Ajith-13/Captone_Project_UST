namespace CaptoneProject.Services.AssignmentAPI.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string LearnerId { get; set; }
        public string FilePath { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public int MarksScored { get; set; }

        public int AssignmentQuestionId { get; set; }
        public AssignmentQuestion AssignmentQuestion { get; set; }
    }
}
