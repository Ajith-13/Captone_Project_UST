namespace CaptoneProject.Services.AssignmentAPI.Data.Dto.Assignment
{
    public class AssignmentResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalMarks { get; set; }
        public int ModuleId { get; set; }
    }
}
