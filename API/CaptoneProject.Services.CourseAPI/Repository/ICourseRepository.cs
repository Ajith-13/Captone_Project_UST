using CaptoneProject.Services.CourseAPI.Data.Dto;
using CaptoneProject.Services.CourseAPI.Models;

namespace CaptoneProject.Services.CourseAPI.Repository
{
    public interface ICourseRepository
    {
        Task<Course> AddCourse(Course course);
        Task<IEnumerable<Course>> GetAllCourse();
        Task<Course> GetById(int id);
        Task<IEnumerable<Course>> GetCoursesByTrainer(string trainerId);
        Task<Course?> Update(int courseId, Course course, string trainerId);
        Task<bool> Delete(int id, string trainerId);
    }
}
