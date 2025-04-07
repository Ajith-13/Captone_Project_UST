using AutoMapper;
using CaptoneProject.Services.CourseAPI.Data.Dto.Course;
using CaptoneProject.Services.CourseAPI.Models;
using CaptoneProject.Services.CourseAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaptoneProject.Services.CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseController(ICourseRepository courseRepository,IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CourseDto courseDto, [FromQuery] string trainerId)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDto);
                var createdCourse = await _courseRepository.AddCourse(course, trainerId);
                var response = _mapper.Map<CourseResponseDto>(createdCourse);
                return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, response);
            } 
            catch(Exception ex)
            {
                return StatusCode(500, $"Error creating the course:{ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var courses = await _courseRepository.GetAllCourse();
                var response = _mapper.Map<IEnumerable<CourseResponseDto>>(courses);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error retrieving courses: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var course = await _courseRepository.GetById(id);
                if (course == null) return NotFound("Course not found.");
                var response = _mapper.Map<CourseResponseDto>(course);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error retrieving course: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseDto courseDto, [FromQuery] string trainerId)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDto);
                var updated = await _courseRepository.Update(id, course, trainerId);
                if (updated == null) return NotFound("Course not found or access denied.");
                var response = _mapper.Map<CourseResponseDto>(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating course: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string trainerId)
        {
            try
            {
                var deleted = await _courseRepository.Delete(id, trainerId);
                if (!deleted) return NotFound("Course not found or access denied.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting course: {ex.Message}");
            }
        }
    }
}
