using CaptoneProject.Services.AssignmentAPI.Models;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAllAssignment();
        Task<Assignment> GetAssignmentById(int id);
        Task<Assignment> AddAssignment(Assignment assignment);
        Task<Assignment> UpdateAssignment(int assignmentId,Assignment assignment,string trainerId);
        Task<bool> DeleteAssignment(string trainerId,int assignmentId);
    }
}
