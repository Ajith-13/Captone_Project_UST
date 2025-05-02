using CaptoneProject.Services.AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.AssignmentAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AssignmentQuestion> AssignmentQuestions { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Streak> Streaks { get; set; }
        public DbSet<LeaderBoard> Leaderboards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assignment>()
                        .HasOne(a => a.AssignmentQuestion)
                        .WithMany(q => q.Assignments)
                        .HasForeignKey(a => a.AssignmentQuestionId) 
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
