using CaptoneProject.Services.CourseAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace CaptoneProject.Services.CourseAPI.Data.Dto.Module
{
    public class ModuleDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? File { get; set; } 
    }
}
