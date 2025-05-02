using CaptoneProject.Services.CourseAPI.Data.Dto.Module;
using CaptoneProject.Services.CourseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaptoneProject.Services.CourseAPI.Repository
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetModulesByCourseId(int courseId);
        Task<Module> GetModuleById(int id);
        Task<Module> AddModule([FromBody] Module dto);
        Task<Module> UpdateModule(int id, [FromBody] Module dto);
        Task<bool> Delete(int id);
    }
}
