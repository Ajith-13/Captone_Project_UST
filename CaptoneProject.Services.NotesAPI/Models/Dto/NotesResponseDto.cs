using System.ComponentModel.DataAnnotations;

namespace CaptoneProject.Services.NotesAPI.Models.Dto
{
    public class NotesResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";

    }
}
