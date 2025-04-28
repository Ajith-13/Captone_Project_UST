using CaptoneProject.Services.AssignmentAPI.Models;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public interface IAssignmentQuestionRepository
    {
        Task<IEnumerable<AssignmentQuestion>> GetAllAssignmentQuestion();
        Task<AssignmentQuestion> GetAssignmentQuestionById(int id);
        Task<AssignmentQuestion> AddAssignmentQuestion(AssignmentQuestion assignment);
        Task<AssignmentQuestion> UpdateAssignmentQuestion(int assignmentId,AssignmentQuestion assignment,string trainerId);
        Task<bool> DeleteAssignmentQuestion(string trainerId,int assignmentId);
        Task<IEnumerable<AssignmentQuestion>> GetAssignmentsByModuleId(int moduleId);
    }
}
