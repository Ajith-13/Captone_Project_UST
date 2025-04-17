
using AutoMapper;
using CaptoneProject.Services.ProfileAPI.Models;
using CaptoneProject.Services.ProfileAPI.Models.Dto;
using CaptoneProject.Services.ProfileAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Profile = CaptoneProject.Services.ProfileAPI.Models.Profile;

namespace CaptoneProject.Services.ProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileAPIController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        private IMapper _mapper;
        public ProfileAPIController(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            try
            {

                var profile = await _profileRepository.GetAllProfiles();
                var response = _mapper.Map<IEnumerable<ResponseDto>>(profile);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving notes: {ex.Message}");
            }
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TRAINER,LEARNER")]

        public async Task<IActionResult> CreateProfile(ProfileDto profileDto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Profile profile = _mapper.Map<Profile>(profileDto);
                profile.UserId=userId;

                var created = await _profileRepository.CreateProfile(profile, userId);
                var response = _mapper.Map<ResponseDto>(created);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating note: {ex.Message}");
            }
        }
    }
}
