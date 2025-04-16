using AutoMapper;
using CaptoneProject.Services.AssignmentAPI.Data.Dto.Assignment;
using CaptoneProject.Services.AssignmentAPI.Models;
using CaptoneProject.Services.AssignmentAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaptoneProject.Services.AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentQuestionController : ControllerBase
    {
        private readonly IAssignmentQuestionRepository _assignmentQuestionRepository;
        private readonly IMapper _mapper;
        public AssignmentQuestionController(IAssignmentQuestionRepository assignmentQuestionRepository,IMapper mapper)
        {
            _assignmentQuestionRepository = assignmentQuestionRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddAssignment([FromBody] AssignmentQuestionDto assignmentDto,string trainerId)
        {
            try
            {
                var assignmentQuestion = _mapper.Map<AssignmentQuestion>(assignmentDto);
                assignmentQuestion.TrainerId = trainerId;
                await _assignmentQuestionRepository.AddAssignmentQuestion(assignmentQuestion);
                var response = _mapper.Map<AssignmentQuestionResponseDto>(assignmentQuestion);
                return CreatedAtAction(nameof(GetAssignmentById), new { id = assignmentQuestion.Id }, response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error creating the Assignment:{ex.Message}");
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentById(int id)
        {
            try
            {
                var assignmentQuestion = await _assignmentQuestionRepository.GetAssignmentQuestionById(id);
                if (assignmentQuestion == null) return NotFound("Assignment not found.");
                var response = _mapper.Map<AssignmentQuestionResponseDto>(assignmentQuestion);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error retrieving Assignment By id: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] AssignmentQuestionDto assignmentDto,[FromQuery] string trainerId)
        {
            try
            {
                var existing = await _assignmentQuestionRepository.GetAssignmentQuestionById(id);
                if (existing == null) return NotFound("Assignment not found.");

                var updated= _mapper.Map<Models.AssignmentQuestion>(assignmentDto);
                updated.Id=existing.Id;
                await _assignmentQuestionRepository.UpdateAssignmentQuestion(id,updated,trainerId);
                var response = _mapper.Map<AssignmentQuestionResponseDto>(updated);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error updating assignment: {ex.Message}");
            }
        }
        [HttpDelete("{assignmentId}")]
        public async Task<IActionResult> Delete(int assignmentId, [FromQuery] string trainerId)
        {
            try
            {
                var deleted = await _assignmentQuestionRepository.DeleteAssignmentQuestion(trainerId,assignmentId);
                if (!deleted) return NotFound("Assignment not found or access denied.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting Assignment: {ex.Message}");
            }
        }

    }
}
