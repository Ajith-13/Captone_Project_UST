using CaptoneProject.Services.AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.AssignmentAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentSubmission> Submissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assignment>()
                .HasMany(a => a.Submissions)
                .WithOne(s => s.Assignment)
                .HasForeignKey(s => s.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
