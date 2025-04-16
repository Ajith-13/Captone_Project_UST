using CaptoneProject.Services.AuthAPI.Data.Dto;
using CaptoneProject.Services.AuthAPI.JwtTokenGenerationService;
using CaptoneProject.Services.AuthAPI.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CaptoneProject.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtService _jwtService;
        public AuthController(UserManager<ApplicationUser> userManager, JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest("Email already in use.");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest("User creation failed.");
            }

            var role = model.Role.ToLower();
            if (role == "trainer")
            {
                await _userManager.AddToRoleAsync(user, "Trainer");
            }
            else if (role == "learner")
            {
                await _userManager.AddToRoleAsync(user, "Learner");
            }
            else
            {
                return BadRequest("Invalid role specified.");
            }

            return Ok("User created successfully.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = _jwtService.GenerateJwtToken(user);

            return Ok(token);
        }
        [HttpGet("{trainerId}")]
        [Authorize(Roles ="TRAINER")]
        public async Task<IActionResult> Get(string trainerId)
        {
            var currentTrainerId = User.Identity.Name;
            Console.WriteLine(currentTrainerId);
            if (trainerId != currentTrainerId)
            {
                return Unauthorized("You can only access your own data.");
            }
            return Ok(new { TrainerId = trainerId, Details = "Trainer-specific data" });
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="TRAINER")]
        public IActionResult GetTrainerDetails()
        {
            var currentUserId = User.Identity.Name;
            Console.WriteLine($"{currentUserId}");
            return Ok(currentUserId);
        }
        [HttpGet("learner")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles = "LEARNER")]
        public IActionResult GetLearnerDetails()
        {
            var currentUserName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(currentUserName);
        }
    }
}
