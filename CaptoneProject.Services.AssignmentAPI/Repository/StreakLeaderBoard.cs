using CaptoneProject.Services.AssignmentAPI.Data;
using CaptoneProject.Services.AssignmentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public class StreakLeaderBoard : IStreakLeaderBoard
    {
        private readonly AppDbContext _dbContext;
        public StreakLeaderBoard(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IActionResult> GetLeaderboard()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStreakAsync(string learnerId)
        {
            var streak = await _dbContext.Streaks.SingleOrDefaultAsync(s => s.LearnerId == learnerId);
            if (streak == null)
            {
                streak = new Streak { LearnerId = learnerId, LastInteractionDate = DateTime.Today, CurrentStreak = 1 };
                _dbContext.Streaks.Add(streak);
            }
            else
            {
                if (streak.LastInteractionDate == DateTime.Today.AddDays(-1))
                {
                    streak.CurrentStreak++;
                }
                else if (streak.LastInteractionDate < DateTime.Today.AddDays(-1))
                {
                    streak.CurrentStreak = 1;
                }

                streak.LastInteractionDate = DateTime.Today;
            }
            await _dbContext.SaveChangesAsync();
        }
        public int CalculateStreakPoints(int streak)
        {
            return streak * (streak + 1) / 2;
        }
        public async Task UpdateLeaderboardAsync(string learnerId)
        {
            var streak = await _dbContext.Streaks.SingleOrDefaultAsync(s => s.LearnerId == learnerId);
            var assignments = await _dbContext.Assignments.Where(a => a.LearnerId == learnerId).ToListAsync();

            int streakPoints = CalculateStreakPoints(streak.CurrentStreak);
            int assignmentMarks = assignments.Sum(a => a.MarksScored);

            var leaderboard = await _dbContext.Leaderboards.SingleOrDefaultAsync(l => l.LearnerId == learnerId);
            if (leaderboard == null)
            {
                leaderboard = new LeaderBoard
                {
                    LearnerId = learnerId,
                    StreakPoints = streakPoints,
                    AssignmentMarks = assignmentMarks
                };
                _dbContext.Leaderboards.Add(leaderboard);
            }
            else
            {
                leaderboard.StreakPoints = streakPoints;
                leaderboard.AssignmentMarks = assignmentMarks;
                leaderboard.TotalMarks = streakPoints + assignmentMarks;
            }

            leaderboard.TotalMarks = leaderboard.StreakPoints + leaderboard.AssignmentMarks;

            await _dbContext.SaveChangesAsync();
            await UpdateLeaderboardRankAsync();
        }

        public async Task UpdateLeaderboardRankAsync()
        {
            var leaderboardList = await _dbContext.Leaderboards.OrderByDescending(l => l.TotalMarks).ToListAsync();
            for (int i = 0; i < leaderboardList.Count; i++)
            {
                leaderboardList[i].Rank = i + 1;
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<LeaderBoard>> GetLeaderboardAsync()
        {
            return await _dbContext.Leaderboards.OrderBy(l => l.Rank).ToListAsync();
        }
    }
}
