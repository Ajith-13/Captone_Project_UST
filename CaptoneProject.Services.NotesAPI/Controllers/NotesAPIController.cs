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

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                
                var notes = await _notesRepository.GetAllNotes();
                var response = _mapper.Map<IEnumerable<NotesResponseDto>>(notes);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving notes: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotesByUser(int userId)
        {
            try
            {
                var notes = await _notesRepository.GetNotesByUser(userId);
                var response = _mapper.Map<IEnumerable<NotesResponseDto>>(notes);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving notes: {ex.Message}");
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateNote( NotesDto notesDto, int userid)
        {
            try
            {
                var notes = _mapper.Map<Notes>(notesDto);
                var creatednotes = await _notesRepository.CreateNote(notes, userid);
                var response = _mapper.Map<NotesResponseDto>(creatednotes);
                return CreatedAtAction(nameof(GetNotesByUser), new { id = creatednotes.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating the notes:{ex.Message}");
            }
        }
        
        
        [HttpPut("{userid}")]
        public async Task<IActionResult> UpdateNote(  int userid, NotesDto notesDto)
        {
            try
            {
                var notes = _mapper.Map<Notes>(notesDto);
                var updated = await _notesRepository.UpdateNote(userid, notes);
                if (updated == null) return NotFound("Notes not found or access denied.");
                var response = _mapper.Map<NotesResponseDto>(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating notes: {ex.Message}");
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteNote(int Id)
        {
            try
            {
                var deleted = await _notesRepository.DeleteNote(Id);
                if (!deleted) return NotFound("Course not found or access denied.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting notes: {ex.Message}");
            }
        }


    }
}
