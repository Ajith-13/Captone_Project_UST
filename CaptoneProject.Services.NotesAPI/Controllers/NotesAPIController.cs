using AutoMapper;
using CaptoneProject.Services.NotesAPI.Data;
using CaptoneProject.Services.NotesAPI.Models;
using CaptoneProject.Services.NotesAPI.Models.Dto;
using CaptoneProject.Services.NotesAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("{user}")]
        //public async Task<IActionResult> GetNotesByUser(string userId)
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> GetNotesByUser()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER,LEARNER")]

        public async Task<IActionResult> CreateNote( NotesDto notesDto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var notes = _mapper.Map<Notes>(notesDto);
                notes.UserId = userId;
                notes.DateCreated = DateTime.UtcNow;
                notes.DateModified = DateTime.UtcNow;

                var created = await _notesRepository.CreateNote(notes, userId);
                var response = _mapper.Map<NotesResponseDto>(created);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating note: {ex.Message}");
            }
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, NotesDto notesDto)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var notes = _mapper.Map<Notes>(notesDto);
                notes.Id = id;
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
