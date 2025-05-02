namespace CaptoneProject.Services.AuthAPI.Data.Dto
{
    public class TrainerRegistrationDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? Certificate { get; set; }
        public IFormFile? Resume { get; set; }
    }
}
