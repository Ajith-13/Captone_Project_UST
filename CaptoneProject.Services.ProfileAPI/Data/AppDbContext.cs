using CaptoneProject.Services.ProfileAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CaptoneProject.Services.ProfileAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profile { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Profile>().HasData(new Profile
            {
                UserId = "1",
                userName = "Ajith",
                rollNumber = 6655,
                PhoneNumber = "1234567",
                email = "Ajith@123",
                rank = 1,
                NumberOfCourcesEnrolled=2,
                CoursesNames = new List<string> { "python","C++"}

            });
        }
    }
}
