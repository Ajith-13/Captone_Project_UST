using Microsoft.AspNetCore.Identity;

namespace CaptoneProject.Services.AuthAPI.User
{
    public class ApplicationUser : IdentityUser
    {
        public string? ApprovalStatus { get; set; } = "pending";
        public string? CertificatePath { get; set; }
        public string? ResumePath { get; set; }
    }
}
