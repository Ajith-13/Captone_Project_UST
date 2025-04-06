using CaptoneProject.Services.CourseAPI.Models;

namespace CaptoneProject.Services.CourseAPI.Data.Dto.Module
{
    public class ModuleResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ModuleContentType ContentType { get; set; }

        public string ContentData { get; set; }

        public string? AssignmentId { get; set; }

        public string? QuizId { get; set; }

        public int CourseId { get; set; }

        //public string CourseTitle { get; set; } // optional extra info/

        public DateTime CreatedAt { get; set; }
    }
}
