using CaptoneProject.Services.ProfileAPI.Models;

namespace CaptoneProject.Services.ProfileAPI.Repository
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetAllProfiles();
        Task<Profile> CreateProfile( Profile profile, string userid);
    }
}
