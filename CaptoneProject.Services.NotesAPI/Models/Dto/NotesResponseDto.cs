using System.ComponentModel.DataAnnotations;

namespace CaptoneProject.Services.NotesAPI.Models.Dto
{
    public class NotesResponseDto
    {
        //[Required]
        //public string UserId { get; set; }
        //[Key]
        //public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Resources { get; set; }
        
    }
}
