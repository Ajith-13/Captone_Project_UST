using System.ComponentModel.DataAnnotations;

namespace CaptoneProject.Services.CourseAPI.Models
{
    public class Module
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public ModuleContentType ContentType { get; set; }
        public string ContentData { get; set; }

        public string? AssignmentId { get; set; }
        public string? QuizId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
    public enum ModuleContentType
    {
        Video,
        Text
    }
}
