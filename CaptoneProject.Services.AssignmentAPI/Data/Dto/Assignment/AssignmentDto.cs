namespace CaptoneProject.Services.AssignmentAPI.Data.Dto.Assignment
{
    public class AssignmentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalMarks { get; set; }
        public int CourseId { get; set; }
        public int ModuleId { get; set; }
    }
}
