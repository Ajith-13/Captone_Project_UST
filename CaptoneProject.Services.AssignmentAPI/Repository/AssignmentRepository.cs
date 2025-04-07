using CaptoneProject.Services.AssignmentAPI.Data;
using CaptoneProject.Services.AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        public readonly AppDbContext _context;
        public AssignmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Assignment>> GetAllAssignmentsByQuestionId(int assignmentQuestionId)
        {
            return await _context.Assignments
                .Where(a => a.AssignmentQuestionId == assignmentQuestionId)
                .ToListAsync();
        }

        public async Task<Assignment> GetAssignmentById(int id)
        {
            return await _context.Assignments
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Assignment> SubmitAssignment(Assignment assignmentSubmission)
        {
            await _context.Assignments.AddAsync(assignmentSubmission);
            await _context.SaveChangesAsync();
            return assignmentSubmission;
        }
    }
}
