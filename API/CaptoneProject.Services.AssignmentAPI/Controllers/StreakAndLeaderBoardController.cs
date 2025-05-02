using CaptoneProject.Services.AssignmentAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaptoneProject.Services.AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreakAndLeaderBoardController : ControllerBase
    {
        private readonly IStreakLeaderBoard _repository;
        public StreakAndLeaderBoardController(IStreakLeaderBoard repository) 
        { 
            _repository = repository;
        }
        [HttpPost("update-streak/{learnerId}")]
        public async Task<IActionResult> UpdateStreak(string learnerId)
        {
            await _repository.UpdateStreakAsync(learnerId);
            await _repository.UpdateLeaderboardAsync(learnerId);
            return Ok(new { message = "Streak and leaderboard updated successfully." });
        }
        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            var leaderboard = await _repository.GetLeaderboardAsync();
            return Ok(leaderboard);
        }
    }
}
