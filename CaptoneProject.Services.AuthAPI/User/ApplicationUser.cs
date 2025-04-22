using Microsoft.AspNetCore.Identity;

namespace CaptoneProject.Services.AuthAPI.User
{
    public class ApplicationUser:IdentityUser
    {
        public string? CertificatePath { get; set; }
        public string? ResumePath { get; set; }
        public string ApprovalStatus { get; set; } = "Pending";
    }
}
