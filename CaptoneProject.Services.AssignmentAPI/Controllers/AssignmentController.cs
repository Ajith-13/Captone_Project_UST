using AutoMapper;
using CaptoneProject.Services.AssignmentAPI.Data.Dto.AssignmentSubmission;
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
        public async Task<IActionResult> SubmitAssignment([FromForm] AssignmentDto assignmentDto)
        {
            try
            {
                var assignment = _mapper.Map<Assignment>(assignmentDto);
                await _assignmentRepository.AddAssignment(assignment);
                var response = _mapper.Map<AssignmentResponseDto>(assignment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAssignment()
        {
            try
            {
                var allAssignments = await _assignmentRepository.GetAllAssignment();
                var response = _mapper.Map<IEnumerable<AssignmentResponseDto>>(allAssignments);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Cannot return assignment"+ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByAssignmentId(int id)
        {
            try
            {
                var assignment = await _assignmentRepository.GetAssignmentById(id);
                var response = _mapper.Map<AssignmentResponseDto>(assignment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
