using CaptoneProject.Services.CourseAPI.Models;

namespace CaptoneProject.Services.CourseAPI.Data.Dto.Module
{
    public class ModuleResponseDto
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string? Description { get; set; } 
        public string? FilePath { get; set; }
        public string? FileType { get; set; } 
        public int CourseId { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
