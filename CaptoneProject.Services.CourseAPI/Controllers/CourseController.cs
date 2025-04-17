using AutoMapper;
using CaptoneProject.Services.CourseAPI.Data.Dto;
using CaptoneProject.Services.CourseAPI.Data.Dto.Course;
using CaptoneProject.Services.CourseAPI.Models;
using CaptoneProject.Services.CourseAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CaptoneProject.Services.CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly ResponseDto _response;
        public CourseController(ICourseRepository courseRepository,IMapper mapper,IWebHostEnvironment environment)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _environment = environment;
            _response = new ResponseDto();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> AddCourse([FromForm] CourseDto courseDto)
        {
            try
            {
                var trainerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(trainerId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Trainer Id Not found";
                    return Unauthorized(_response);
                }
                if (string.IsNullOrEmpty(_environment.WebRootPath))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Server Configuration error";
                    return StatusCode(500,_response);
                }
                string? thumbnailPath = await UploadThumbnailAsync(courseDto.ThumbnailImage);
           
                var course = _mapper.Map<Course>(courseDto);
                course.TrainerId = trainerId;
                course.ThumbnailImagePath = thumbnailPath;
                var createdCourse = await _courseRepository.AddCourse(course);
                var response = _mapper.Map<CourseResponseDto>(createdCourse);
                _response.IsSuccess = true;
                _response.Message = "Course Added Successfully";
                _response.Data = response;
            } 
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Exception"+ex.Message;
                return StatusCode(500,_response);
            }
            return Ok(_response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var courses = await _courseRepository.GetAllCourse();
                var response = _mapper.Map<IEnumerable<CourseResponseDto>>(courses);
                _response.IsSuccess = true;
                _response.Message = "All courses";
                _response.Data = response;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Exception" + ex.Message;
                return StatusCode(500,_response);
            }
        }
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var course = await _courseRepository.GetById(id);
                if (course == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Course Cannot be found";
                    return NotFound(_response);
                }
                var response = _mapper.Map<CourseResponseDto>(course);
                _response.IsSuccess = true;
                _response.Message = "Success";
                _response.Data = response;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Esception" + ex.Message;
                return StatusCode(500,_response);
            }
        }
        [HttpPut("{courseId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> Update(int courseId, [FromForm] CourseDto courseDto)
        {
            try
            {
                var trainerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(trainerId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Trainer Id Not found";
                    return Unauthorized(_response);
                }
                if (string.IsNullOrEmpty(_environment.WebRootPath))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Server configuration error: WebRootPath is not set.";
                    return StatusCode(500, _response);
                }
                string? thumbnailPath = await UploadThumbnailAsync(courseDto.ThumbnailImage);
               
                var course = _mapper.Map<Course>(courseDto);
                course.ThumbnailImagePath = thumbnailPath;
                var updated = await _courseRepository.Update(courseId, course, trainerId);
                if (updated == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not able to update";
                return NotFound(_response);
                }
                var response = _mapper.Map<CourseResponseDto>(updated);
                _response.IsSuccess = true;
                _response.Message = "Updated Successfully";
                _response.Data = response;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Excpetion" + ex.Message;
                return StatusCode(500,_response);
            }
        }
        [HttpDelete("{CourseId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> Delete(int CourseId)
        {
            try
            {
                var trainerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(trainerId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Trainer Id Not found";
                    return Unauthorized(_response);
                }
                var deleted = await _courseRepository.Delete(CourseId, trainerId);
                if (!deleted)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Failed: Course not found to delete";
                    return NotFound(_response);
                }
                _response.IsSuccess = true;
                _response.Message = "Deleted Successfully";
                _response.Data = deleted;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Exception" + ex.Message;
                return StatusCode(500,_response);
            }
        }
        private async Task<string?> UploadThumbnailAsync(IFormFile thumbnailImage)
        {
            if (thumbnailImage == null)
                return null;

            if (string.IsNullOrEmpty(_environment.WebRootPath))
                throw new InvalidOperationException("WebRootPath is not configured.");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "thumbnails");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{thumbnailImage.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await thumbnailImage.CopyToAsync(fileStream);
            }

            return $"/thumbnails/{uniqueFileName}";
        }
    }
}
