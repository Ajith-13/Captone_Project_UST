using System.ComponentModel.DataAnnotations;

namespace CaptoneProject.Services.ProfileAPI.Models.Dto
{
    public class ProfileDto
    {
        [Key]
        public string UserId { get; set; }
        public string userName { get; set; }
        public int rollNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string email { get; set; }
        public int rank { get; set; }
        public int NumberOfCourcesEnrolled { get; set; }
        public List<string> CoursesNames { get; set; }
    }
}
