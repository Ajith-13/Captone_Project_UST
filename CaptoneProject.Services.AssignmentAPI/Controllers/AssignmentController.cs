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
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;
        public AssignmentController(IAssignmentRepository assignmentRepository,IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddAssignment([FromBody] AssignmentDto assignmentDto,string trainerId)
        {
            try
            {
                var assignment = _mapper.Map<Assignment>(assignmentDto);
                assignment.TrainerId = trainerId;
                await _assignmentRepository.AddAssignment(assignment);
                var response = _mapper.Map<AssignmentResponseDto>(assignment);
                return CreatedAtAction(nameof(GetAssignmentById), new { id = assignment.Id }, response);
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
                var assignment = await _assignmentRepository.GetAssignmentById(id);
                if (assignment == null) return NotFound("Assignment not found.");
                var response = _mapper.Map<AssignmentResponseDto>(assignment);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error retrieving Assignment By id: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] AssignmentDto assignmentDto,[FromQuery] string trainerId)
        {
            try
            {
                var existing = await _assignmentRepository.GetAssignmentById(id);
                if (existing == null) return NotFound("Assignment not found.");

                var updated=_mapper.Map<Assignment>(assignmentDto);
                await _assignmentRepository.UpdateAssignment(id,updated,trainerId);
                var response = _mapper.Map<AssignmentResponseDto>(updated);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error updating assignment: {ex.Message}");
            }
        }

    }
}
