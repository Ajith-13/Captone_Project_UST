using CaptoneProject.Services.AssignmentAPI.Models;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAllAssignmentsByQuestionId(int assignmentQuestionId);
        Task<Assignment> GetAssignmentById(int id);
        Task<Assignment> SubmitAssignment(Assignment assignmentSubmission);
    }
}
