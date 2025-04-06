using CaptoneProject.Services.CourseAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace CaptoneProject.Services.CourseAPI.Data.Dto.Module
{
    public class ModuleDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public ModuleContentType ContentType { get; set; }

        public string ContentData { get; set; }

        public string? AssignmentId { get; set; }

        public string? QuizId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
