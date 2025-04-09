using CaptoneProject.Services.NotesAPI.Models;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace CaptoneProject.Services.NotesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Notes> Notes { get; set; }  // Define the DbSet for Note entity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notes>().HasData(new Notes
            {
                Id = 1,
                Title = "Python",
                Description = "Python programming is a high-level, general-purpose, interpreted, object-oriented programming" +
                " language known for its readability and versatility," +
                " used for various tasks like web development, data analysis," +
                " and software development.",
                Resources = "GeeksForGeeks",
                UserId="1"

            });
        }
    }
}
