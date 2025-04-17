using CaptoneProject.Services.ProfileAPI.Data;
using CaptoneProject.Services.ProfileAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.ProfileAPI.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;

        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Profile>> GetAllProfiles()
        {
            //while;dbfdnc
            return await _context.Profile.ToListAsync();
        }
        public async Task<Profile> CreateProfile(Profile profile, string userid)
        {
            Profile n = new Profile()
            {
                UserId = userid,
                userName = profile.userName,
                rollNumber = profile.rollNumber,
                PhoneNumber = profile.PhoneNumber,
                email = profile.email,
                rank = profile.rank,
                NumberOfCourcesEnrolled=profile.NumberOfCourcesEnrolled,
                CoursesNames=profile.CoursesNames

            };
            _context.Profile.Add(n);
            await _context.SaveChangesAsync();
            return n;
        }
    }
}
