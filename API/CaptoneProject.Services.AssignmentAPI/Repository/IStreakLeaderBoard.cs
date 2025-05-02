using CaptoneProject.Services.AssignmentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public interface IStreakLeaderBoard
    {
        Task<IActionResult> GetLeaderboard();
        Task UpdateStreakAsync(string learnerId);
        Task UpdateLeaderboardAsync(string learnerId);
        Task UpdateLeaderboardRankAsync();
        Task<List<LeaderBoard>> GetLeaderboardAsync();
    }
}
