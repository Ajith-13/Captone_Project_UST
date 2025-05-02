namespace CaptoneProject.Services.AssignmentAPI.Data.Dto
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
        public string Message { get; set; }
    }
}
