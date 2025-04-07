namespace CaptoneProject.Services.AssignmentAPI.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public int TotalMarks { get; set; }
        public int CourseId { get; set; }

        public int ModuleId { get; set; }
        public string TrainerId { get; set; }

        public ICollection<AssignmentSubmission> Submissions { get; set; }
    }
}
