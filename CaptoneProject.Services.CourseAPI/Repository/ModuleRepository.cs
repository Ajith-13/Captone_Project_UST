using CaptoneProject.Services.CourseAPI.Data;
using CaptoneProject.Services.CourseAPI.Data.Dto.Module;
using CaptoneProject.Services.CourseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.CourseAPI.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly AppDbContext _context;
        public ModuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Module> AddModule([FromBody] Module module)
        {
            var course = await _context.Courses.FindAsync(module.CourseId);
            if (course == null)
                return null;

            _context.Modules.Add(module);
            await _context.SaveChangesAsync();

            return module;
        }

        public async Task<bool> Delete(int id)
        {
            var module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id);

            if (module == null)
                return false;

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Module> GetModuleById(int id)
        {
            var module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id);

            if (module == null)
                return null;

            return module;

        }

        public async Task<IEnumerable<Module>> GetModulesByCourseId(int courseId)
        {
            var modules = await _context.Modules
                 .Where(m => m.CourseId == courseId)
                 .ToListAsync();
            return modules;
        }

        public async Task<Module> UpdateModule(int id, [FromBody] Module module)
        {
            var existingModule = await _context.Modules
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingModule is null)
                return null;
            existingModule.Title = module.Title;
            if(module.FilePath!=null)
            {
                existingModule.FilePath = module.FilePath;
            }
            existingModule.Description = module.Description;
            existingModule.AssignmentId = module.AssignmentId;
            existingModule.QuizId = module.QuizId;
            existingModule.CourseId = module.CourseId;

            await _context.SaveChangesAsync();
            return existingModule;
        }
    }
}
