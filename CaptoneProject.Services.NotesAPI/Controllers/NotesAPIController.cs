using AutoMapper;
using CaptoneProject.Services.NotesAPI.Data;
using CaptoneProject.Services.NotesAPI.Models;
using CaptoneProject.Services.NotesAPI.Models.Dto;
using CaptoneProject.Services.NotesAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaptoneProject.Services.NotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesAPIController : ControllerBase
    {
        private readonly INotesRepository _notesRepository;
        private IMapper _mapper;
        public NotesAPIController(INotesRepository notesRepository, IMapper mapper)
        {
            _notesRepository = notesRepository;
            _mapper = mapper;
        }



        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] NotesDto notesDto, [FromQuery] int Id)
        {
            try
            {
                var notes = _mapper.Map<Notes>(notesDto);
                var creatednotes = await _notesRepository.CreateNote(notes, Id);
                var response = _mapper.Map<NotesResponseDto>(creatednotes);
                return CreatedAtAction(nameof(GetById), new { id = creatednotes.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating the course:{ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var courses = await _notesRepository.GetAllCourse();
                var response = _mapper.Map<IEnumerable<NotesResponseDto>>(courses);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving courses: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var course = await _notesRepository.GetById(id);
                if (course == null) return NotFound("Course not found.");
                var response = _mapper.Map<NotesResponseDto>(course);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving course: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NotesDto courseDto, [FromQuery] string trainerId)
        {
            try
            {
                var course = _mapper.Map<Notes>(courseDto);
                var updated = await _notesRepository.Update(id, course, trainerId);
                if (updated == null) return NotFound("Course not found or access denied.");
                var response = _mapper.Map<NotesResponseDto>(updated);
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
                var deleted = await _notesRepository.Delete(id, trainerId);
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
