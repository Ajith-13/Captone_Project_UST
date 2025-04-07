namespace CaptoneProject.Services.AssignmentAPI.Models
{
    public class AssignmentSubmission
    {
        public int Id { get; set; }
        public string LearnerId { get; set; }
        public string FilePath { get; set; }
        public int MarksScored { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
