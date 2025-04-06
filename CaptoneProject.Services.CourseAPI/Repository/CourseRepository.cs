using CaptoneProject.Services.CourseAPI.Data;
using CaptoneProject.Services.CourseAPI.Data.Dto;
using CaptoneProject.Services.CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.CourseAPI.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Course> AddCourse(Course course, string trainerId)
        {
            course.TrainerId = trainerId;
            _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> Delete(int id, string trainerId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id && c.TrainerId == trainerId);
            if(course is null)
            {
                return false;
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Course>> GetAllCourse()
        {
            return await _context.Courses.Include(c => c.Modules).ToListAsync();
        }

        public async Task<Course> GetById(int id)
        {
            return await _context.Courses.Include(c => c.Modules).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course?> Update(int courseId, Course course, string trainerId)
        {
            var existingCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.TrainerId == trainerId);
            if(existingCourse is null)
            {
                return null;
            }
            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            await _context.SaveChangesAsync();
            return existingCourse;
        }
    }
}
