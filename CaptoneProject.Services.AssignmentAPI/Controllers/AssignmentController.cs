using AutoMapper;
using CaptoneProject.Services.AssignmentAPI.Data.Dto.AssignmentSubmission;
using CaptoneProject.Services.AssignmentAPI.Data.Dto.Mark;
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
        private readonly IWebHostEnvironment _environment;

        public AssignmentController(IAssignmentRepository assignmentRepository,IMapper mapper, IWebHostEnvironment environment)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
            _environment = environment;
        }
        [HttpPost]
        public async Task<IActionResult> SubmitAssignment([FromForm] AssignmentDto assignmentDto)
        {
            try
            {
                var assignment = _mapper.Map<Assignment>(assignmentDto);
                string? assignmentPath = await UploadAssignmentAsync(assignmentDto.FilePath);
                assignment.FilePath = assignmentPath;
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
        [HttpPut("{assignmentId}")]
        public async Task<IActionResult> UpdateMarks(int assignmentId,[FromBody] UpdateMarkDto updateMarkDto)
        {
            try
            {
                if (updateMarkDto == null)
                {
                    return BadRequest("Marks value is required");
                }
                var assignment = await _assignmentRepository.GetAssignmentById(assignmentId);
                if (assignment == null)
                {
                    return NotFound();
                }
                assignment.MarksScored = updateMarkDto.Marks;
                var response = await _assignmentRepository.UpdateMark(assignment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }
        private async Task<string?> UploadAssignmentAsync(IFormFile assignment)
        {
            if (assignment == null)
                return null;

            if (string.IsNullOrEmpty(_environment.WebRootPath))
                throw new InvalidOperationException("WebRootPath is not configured.");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "thumbnails");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{assignment.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await assignment.CopyToAsync(fileStream);
            }

            return $"/assignment/{uniqueFileName}";
        }
    }
}
