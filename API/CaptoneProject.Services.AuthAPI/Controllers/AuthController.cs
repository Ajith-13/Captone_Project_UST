﻿using CaptoneProject.Services.AuthAPI.Data.Dto;
using CaptoneProject.Services.AuthAPI.JwtTokenGenerationService;
using CaptoneProject.Services.AuthAPI.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        [HttpPost("register/trainer")]
        public async Task<IActionResult> RegisterTrainer([FromForm] TrainerRegistrationDto trainerRegistrationDto)
        {
            if (!ModelState.IsValid) return BadRequest(new { ModelState });

            var user = new ApplicationUser
            {
                UserName = trainerRegistrationDto.UserName,
                Email = trainerRegistrationDto.Email,
                ApprovalStatus = "Pending"
            };

            var result = await _userManager.CreateAsync(user, trainerRegistrationDto.Password);
            if (!result.Succeeded) return BadRequest(new { result.Errors });

            if (trainerRegistrationDto.Certificate != null)
            {
                var certificatePath = Path.Combine("Uploads/Certificates", $"{user.Id}_certificate.pdf");
                var directoryPath = Path.GetDirectoryName(certificatePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                using (var stream = new FileStream(certificatePath, FileMode.Create))
                {
                    await trainerRegistrationDto.Certificate.CopyToAsync(stream);
                }
                user.CertificatePath = certificatePath;
            }

            if (trainerRegistrationDto.Resume != null)
            {
                var resumePath = Path.Combine("Uploads/Resumes", $"{user.Id}_resume.pdf");
                var resumeDirectoryPath = Path.GetDirectoryName(resumePath);
                if (!Directory.Exists(resumeDirectoryPath))
                {
                    Directory.CreateDirectory(resumeDirectoryPath); // Ensure directory exists
                }
                using (var stream = new FileStream(resumePath, FileMode.Create))
                {
                    await trainerRegistrationDto.Resume.CopyToAsync(stream);
                }
                user.ResumePath = resumePath;
            }
            await _userManager.AddToRoleAsync(user, "Trainer");
            await _userManager.UpdateAsync(user);

            return Ok(new { message = "Trainer Registered Successfully. Waiting for admin approval" });
        }
        [HttpPost("register/learner")]
        public async Task<IActionResult> SignUp([FromForm] RegisterDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest("Email already in use.");
            }
               existingUser = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (existingUser != null)
            {
                return Conflict("Username is already taken. Please choose another.");
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(errorMessages);
            }

            await _userManager.AddToRoleAsync(user, "Learner");
            return Ok("User created successfully.");
        }
        [HttpGet("all-trainers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<IActionResult> GetAllTrainers()
        {
            // Fetch all users who have the "TRAINER" role, regardless of their approval status
            var allTrainers = await _userManager.Users
                                                .ToListAsync();

            var trainersWithRoles = new List<ApplicationUser>();

            foreach (var user in allTrainers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("TRAINER"))
                {
                    trainersWithRoles.Add(user);
                }
            }

            // Return only the relevant information for each trainer
            return Ok(trainersWithRoles.Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email,
                u.ApprovalStatus,
                u.CertificatePath,
                u.ResumePath
            }));
        }
        [HttpGet("trainer/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<IActionResult> GetTrainerById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "Trainer not found" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("TRAINER"))
            {
                return Forbid(); // or return BadRequest("User is not a trainer.");
            }
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var cleanedCertificatePath = user.CertificatePath?.Replace("\\", "/"); // Normalize slashes

            var fullCertificateUrl = !string.IsNullOrEmpty(cleanedCertificatePath)
                ? $"{baseUrl}/files/Certificates/{Path.GetFileName(cleanedCertificatePath)}"
                : null;

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.ApprovalStatus,
                CertificatePath = fullCertificateUrl,
                ResumePath = !string.IsNullOrEmpty(user.ResumePath)
        ? $"{baseUrl}/files/Resumes/{Path.GetFileName(user.ResumePath.Replace("\\", "/"))}"
        : null
            });
        }

        [HttpPost("approve-trainer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<IActionResult> ApproveTrainer([FromBody] TrainerApprovalDto model)
        {
            var user = await _userManager.FindByIdAsync(model.TrainerId);
            if (user == null)
            {
                return NotFound(new { message = "Trainer not found" }); 
            }

            if (user.ApprovalStatus == "Approved" && !model.IsApproved)
            {
                user.ApprovalStatus = "Pending";
            }
            else if (user.ApprovalStatus == "Pending" && model.IsApproved)
            {
                user.ApprovalStatus = "Approved";
            }
            else
            {
                return BadRequest(new { message = "No change in approval status required" });
            }

            await _userManager.UpdateAsync(user);

            return Ok(new { message = $"Trainer {user.UserName} approval status has been updated to {user.ApprovalStatus.ToLower()}." });
        }



        [HttpGet("pending-trainers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<IActionResult> GetPendingTrainers()
        {
            var pendingTrainers = await _userManager.Users
                                                    .Where(u => u.ApprovalStatus == "Pending")
                                                    .ToListAsync();

            var trainersWithRoles = new List<ApplicationUser>();

            foreach (var user in pendingTrainers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("TRAINER"))
                {
                    trainersWithRoles.Add(user);
                }
            }

            return Ok(trainersWithRoles.Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email,
                u.ApprovalStatus,
                u.CertificatePath,
                u.ResumePath
            }));

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "No User Found. Register First" });
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return Unauthorized(new { message = "Wrong Password" });
            }
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Any(role => role.Equals("Trainer", StringComparison.OrdinalIgnoreCase)))
            {
                if (user.ApprovalStatus != "Approved")
                {
                    return Unauthorized(new { message = "Trainer approval pending." });

                }
            }
           
            var token = _jwtService.GenerateJwtToken(user);

            return Ok(new {token});
        }
        [AllowAnonymous]
        [HttpPost("admin-login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Email Id Not Found");
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return Unauthorized("Invalid credentials.");
            }

            var roles = await _userManager.GetRolesAsync(user);

          
            if (!roles.Any(role => role.Equals("Admin", StringComparison.OrdinalIgnoreCase)))
            {
                return Unauthorized("You must be an admin to access this resource.");
            }


            var token = _jwtService.GenerateJwtToken(user);

            return Ok(new { token });
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
