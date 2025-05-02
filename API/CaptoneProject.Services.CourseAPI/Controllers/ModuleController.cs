
using AutoMapper;
using CaptoneProject.Services.CourseAPI.Data;
using CaptoneProject.Services.CourseAPI.Data.Dto;
using CaptoneProject.Services.CourseAPI.Data.Dto.Module;
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
    public class ModuleController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        private readonly IWebHostEnvironment _environment;

        public ModuleController(ICourseRepository courseRepository,IModuleRepository moduleRepository,IMapper mapper,IWebHostEnvironment environment)
        {
            _courseRepository = courseRepository;
            _moduleRepository = moduleRepository;
            _mapper = mapper;
            _environment = environment;
            _response=new ResponseDto();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> AddModule([FromForm] ModuleDto moduleDto, int courseId)
        {
            try
            {
                var trainerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(trainerId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Trainer Id not found";
                    return Unauthorized(_response);
                }

                var course = await _courseRepository.GetById(courseId);
                if (course == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "There is no course to add the module.Please add the course first";
                    return BadRequest(_response);
                }
                if (course.TrainerId != trainerId)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Unauthorized";
                    return Unauthorized(_response);
                }
                var module = _mapper.Map<Module>(moduleDto);
                module.Course = null;
                module.TrainerId= trainerId;
                module.CourseId = courseId;
                var filePath = await UploadModuleAsync(moduleDto.File);
                module.Description = moduleDto.Description;
                module.FilePath= filePath;
                

                await _moduleRepository.AddModule(module);
                var response = _mapper.Map<ModuleResponseDto>(module);

                _response.IsSuccess = true;
                _response.Message = "Successfully added module";
                _response.Data = response;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Exception: " + ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var module = await _moduleRepository.GetModuleById(id);
                if (module == null)
                {
                    _response.IsSuccess = false;
                    _response.Message="Module Not found";
                    return NotFound(_response);
                }
                var response = _mapper.Map<ModuleResponseDto>(module);
                _response.IsSuccess=true;
                _response.Message = "Success";
                _response.Data=response;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.Message = "Exception" + ex.Message;
                return StatusCode(500,_response);
            }
           
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetModulesByCourseId(int courseId)
        {
            try
            {
                var modules = await _moduleRepository.GetModulesByCourseId(courseId);
                var response = modules.Select(m => _mapper.Map<ModuleResponseDto>(m));
                _response.IsSuccess=true;
                _response.Message = "Success";
                _response.Data=response;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.Message="Exception"+ex.Message;
                return StatusCode(500,_response);
            }
          
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> UpdateModule(int moduleId, [FromForm] ModuleDto moduleDto)
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
                var existingModule = await _moduleRepository.GetModuleById(moduleId);
                if (existingModule == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No Module to update";
                    return BadRequest(_response);
                }
                if(existingModule.TrainerId!=trainerId)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Unauthorized";
                    return Unauthorized(_response);
                }
                var module = _mapper.Map<Module>(moduleDto);
                var filePath = await UploadModuleAsync(moduleDto.File);
                if (filePath != null)
                {
                    module.FilePath = filePath;
                }
                module.CourseId=existingModule.CourseId;
                var updated = await _moduleRepository.UpdateModule(moduleId,module);
                if (updated == null) return NotFound("Module not found.");

                var response = _mapper.Map<ModuleResponseDto>(updated);
                _response.IsSuccess=true;
                _response.Message = "Success";
                _response.Data=response;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess= false;
                _response.Message = "Exception" + ex.Message;
                return StatusCode(500,_response);
            }
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> DeleteModule(int moduleId)
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
                var module = await _moduleRepository.GetModuleById(moduleId);
                if (module == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not found";
                    return NotFound(_response);
                }

                if (module.TrainerId != trainerId)
                {
                    _response.IsSuccess=false;
                    _response.Message = "UnAuthorized";
                    return BadRequest(_response);
                }

                await _moduleRepository.Delete(moduleId);
                _response.IsSuccess=true;
                _response.Message = "Successfully";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message="Exception"+ex.Message;
                return StatusCode(500,_response);
            }
        }
        private async Task<string?> UploadModuleAsync(IFormFile upload)
        {
            if (upload == null)
                return null;

            if (string.IsNullOrEmpty(_environment.WebRootPath))
                throw new InvalidOperationException("WebRootPath is not configured.");

            // Save the file in the 'module' folder inside 'wwwroot'
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "module");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{upload.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await upload.CopyToAsync(fileStream);
            }

            // Return the correct URL for frontend (from /module/)
            return $"/module/{uniqueFileName}";
        }

    }
}
