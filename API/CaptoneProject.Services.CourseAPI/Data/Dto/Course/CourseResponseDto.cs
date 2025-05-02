namespace CaptoneProject.Services.CourseAPI.Data.Dto.Course
{
    public class CourseResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        public string ThumbnailImagePath { get; set; }
    }
}
