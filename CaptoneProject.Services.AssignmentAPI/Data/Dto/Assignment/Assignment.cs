namespace CaptoneProject.Services.AssignmentAPI.Data.Dto.AssignmentSubmission
{
    public class Assignment
    {
        public string LearnerId { get; set; }
        public IFormFile FilePath { get; set; }
        public int AssignmentId { get; set; }
    }
}
