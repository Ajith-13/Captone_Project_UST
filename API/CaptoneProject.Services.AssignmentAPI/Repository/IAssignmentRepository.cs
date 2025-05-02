using CaptoneProject.Services.AssignmentAPI.Models;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAllAssignment();
        Task<Assignment> GetAssignmentById(int id);
        Task<Assignment>AddAssignment(Assignment assignmentSubmission);
        Task<Assignment> UpdateMark(Assignment assignment);
        Task<IEnumerable<AssignmentQuestion>> GetAssignmentsByTrainerIdAsync(string trainerId);
        Task<IEnumerable<Assignment>> GetAssignmentsByLearnerIdAsync(string learnerId);
        Task<AssignmentQuestion> GetAssignmentQuestionByIdAsync(int assignmentQuestionId);
    }
}
