
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
        public ModuleController(ICourseRepository courseRepository,IModuleRepository moduleRepository,IMapper mapper)
        {
            _courseRepository = courseRepository;
            _moduleRepository = moduleRepository;
            _mapper = mapper;
            _response=new ResponseDto();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        [HttpPost]
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
                if (course == null || course.TrainerId != trainerId)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid course or unauthorized access";
                    return BadRequest(_response);
                }
                var module = _mapper.Map<Module>(moduleDto);
                module.Course = course;
                module.CourseId = courseId;
                if (moduleDto.File != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(moduleDto.File.FileName);
                    var filePath = Path.Combine("UploadedFiles", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await moduleDto.File.CopyToAsync(stream);
                    }

                    module.FilePath = filePath;
                    module.FileType = Path.GetExtension(moduleDto.File.FileName).ToLower();
                }
                else
                {
                    module.Description = moduleDto.Description;
                }

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
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> UpdateModule(int courseId, [FromBody] ModuleDto moduleDto)
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
                var course = await _courseRepository.GetById(courseId);
                if (course == null || course.TrainerId != trainerId)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not authorized";
                    return BadRequest(_response);
                }
                var module = _mapper.Map<Module>(moduleDto);
                var updated = await _moduleRepository.UpdateModule(courseId,module);
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
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER")]
        public async Task<IActionResult> DeleteModule(int courseId)
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
                var module = await _moduleRepository.GetModuleById(courseId);
                if (module == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not found";
                    return NotFound(_response);
                }

                var course = await _courseRepository.GetById(courseId);
                if (course == null || course.TrainerId != trainerId)
                {
                    _response.IsSuccess=false;
                    _response.Message = "UnAuthorized";
                    return BadRequest(_response);
                }

                await _moduleRepository.Delete(courseId);
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
    }
}
