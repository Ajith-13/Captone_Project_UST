namespace CaptoneProject.Services.AssignmentAPI.Data.Dto.AssignmentSubmission
{
    public class AssignmentResponseDto
    {
        public int Id { get; set; }
        public string LearnerId { get; set; }
        public int AssignmentQuestionId { get; set; }
        public string FilePath { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int? MarksObtained { get; set; }
    }
}
