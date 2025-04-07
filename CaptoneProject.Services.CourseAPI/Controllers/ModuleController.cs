
using AutoMapper;
using CaptoneProject.Services.CourseAPI.Data;
using CaptoneProject.Services.CourseAPI.Data.Dto.Module;
using CaptoneProject.Services.CourseAPI.Models;
using CaptoneProject.Services.CourseAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaptoneProject.Services.CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;
        public ModuleController(ICourseRepository courseRepository,IModuleRepository moduleRepository,IMapper mapper)
        {
            _courseRepository = courseRepository;
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddModule([FromBody] ModuleDto moduleDto,[FromQuery] string trainerId)
        {
            try
            {
                var course = await _courseRepository.GetById(moduleDto.CourseId);
                if (course == null || course.TrainerId != trainerId)
                {
                    return BadRequest("You are not authorized for it");
                }
                var module = _mapper.Map<Module>(moduleDto);
                module.Course = course;
                await _moduleRepository.AddModule(module);
                var response = _mapper.Map<ModuleResponseDto>(module);
                return CreatedAtAction(nameof(GetById), new {id=module.Id},response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error creating the module:{ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var module = await _moduleRepository.GetModuleById(id);
            if (module == null) return NotFound("Module not found");

            var response = _mapper.Map<ModuleResponseDto>(module);
            return Ok(response);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetModulesByCourseId(int courseId)
        {
            var modules = await _moduleRepository.GetModulesByCourseId(courseId);
            var response = modules.Select(m => _mapper.Map<ModuleResponseDto>(m));
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModule(int id, [FromBody] ModuleDto moduleDto, [FromQuery] string trainerId)
        {
            try
            {
                var course = await _courseRepository.GetById(moduleDto.CourseId);
                if (course == null || course.TrainerId != trainerId)
                {
                    return BadRequest("You are not authorized to update this module.");
                }
                var module = _mapper.Map<Module>(moduleDto);
                var updated = await _moduleRepository.UpdateModule(id,module);
                if (updated == null) return NotFound("Module not found.");

                var response = _mapper.Map<ModuleResponseDto>(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating module: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id, [FromQuery] string trainerId)
        {
            try
            {
                var module = await _moduleRepository.GetModuleById(id);
                if (module == null) return NotFound("Module not found.");

                var course = await _courseRepository.GetById(module.CourseId);
                if (course == null || course.TrainerId != trainerId)
                {
                    return BadRequest("You are not authorized to delete this module.");
                }

                await _moduleRepository.Delete(id);
                return Ok("Module deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting module: {ex.Message}");
            }
        }
    }
}
