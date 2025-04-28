using AutoMapper;
using Azure;
using CaptoneProject.Services.AssignmentAPI.Data.Dto.Assignment;
using CaptoneProject.Services.AssignmentAPI.Data.Dto.AssignmentSubmission;
using CaptoneProject.Services.AssignmentAPI.Data.Dto.Mark;
using CaptoneProject.Services.AssignmentAPI.Models;
using CaptoneProject.Services.AssignmentAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "LEARNER")]
        public async Task<IActionResult> SubmitAssignment([FromForm] AssignmentDto assignmentDto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Unauthorized");
                }
                var assignment = _mapper.Map<Assignment>(assignmentDto);
                string? assignmentPath = await UploadAssignmentAsync(assignmentDto.FilePath);
                assignment.FilePath = assignmentPath;
                assignment.LearnerId= userId;
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
        [HttpGet("TrainerAssignments")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> GetAssignmentsByTrainerId()
        {
            try
            {
                var trainerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(trainerId))
                {
                    return Unauthorized("Unauthorized");
                }

                // Fetch the assignments that belong to the trainer
                var assignments = await _assignmentRepository.GetAssignmentsByTrainerIdAsync(trainerId);

                if (assignments == null || !assignments.Any())
                {
                    return NotFound("No assignments found for this trainer.");
                }

                // Map to the response DTO
                var assignmentResponse = assignments.Select(aq => new
                {
                    AssignmentQuestion = new
                    {
                        aq.Id,
                        aq.Title,
                        aq.Description,
                        aq.UploadDate,
                        aq.DueDate,
                        aq.TotalMarks,
                        // Include related assignments (SubmittedAt, MarksScored, LearnerId)
                        Assignments = aq.Assignments.Select(assign => new
                        {
                            assign.Id,
                            assign.LearnerId,
                            assign.FilePath,
                            assign.SubmittedAt,
                            assign.MarksScored
                        }).ToList() // Convert Assignments to a List
                    }
                }).ToList(); // Convert final result to a list

                return Ok(assignmentResponse);  // Return the mapped response
            }
            catch (Exception ex)
            {
                // Log the error and return 500 Internal Server Error
                //_logger.LogError(ex, "An error occurred while fetching assignments.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("Submitted")] // Adjust the endpoint path as necessary
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "LEARNER")]
        public async Task<IActionResult> GetSubmittedAssignments()
        {
            // Extract user ID from the JWT token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Assuming "NameIdentifier" contains the learner's ID
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Unauthorized access.");
            }

            try
            {
                // Fetch the assignments submitted by the learner using the userId
                var assignments = await _assignmentRepository.GetAssignmentsByLearnerIdAsync(userId);

                if (assignments == null || !assignments.Any())
                {
                    return NotFound("No assignments found for this learner.");
                }

                // Create a list of tasks to fetch assignment questions concurrently
                var assignmentDetails = new List<object>();

                foreach (var a in assignments)
                {
                    var assignmentQuestion = await _assignmentRepository.GetAssignmentQuestionByIdAsync(a.AssignmentQuestionId);
                    assignmentDetails.Add(new
                    {
                        a.Id,
                        a.SubmittedAt,
                        a.MarksScored,
                        a.AssignmentQuestionId,
                        AssignmentQuestion = assignmentQuestion != null
                            ? new { assignmentQuestion.Title, assignmentQuestion.Description, assignmentQuestion.DueDate ,assignmentQuestion.TotalMarks}
                            : null
                    });
                }

                return Ok(assignmentDetails);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (if using logging framework)
                return StatusCode(500, "An error occurred while fetching assignments: " + ex.Message);
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
