using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CaptoneProject.Services.CourseAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string TrainerId { get; set; }
        public string ThumbnailImagePath { get; set; }
        public ICollection<Module> Modules { get; set; } = new List<Module>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
