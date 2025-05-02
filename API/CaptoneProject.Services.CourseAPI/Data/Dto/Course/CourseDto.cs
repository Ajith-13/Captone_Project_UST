namespace CaptoneProject.Services.CourseAPI.Data.Dto.Course
{
    public class CourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
