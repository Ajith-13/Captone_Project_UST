using CaptoneProject.Services.AssignmentAPI.Data;
using CaptoneProject.Services.AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _context;
        public AssignmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Assignment> AddAssignment(Assignment assignment)
        {
            await _context.Assignments.AddAsync(assignment);
            try
            {
            await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(innerMessage);
            }
            return assignment;
        }

        public async Task<bool> DeleteAssignment(string trainerId,int assignmentId)
        {
            var assignment = await _context.Assignments.FirstOrDefaultAsync(a=>a.Id==assignmentId && a.TrainerId==trainerId);
            if (assignment is null)
            {
                return false;
            }
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Assignment>> GetAllAssignment()
        {
            return await _context.Assignments.ToListAsync();

        }

        public async Task<Assignment> GetAssignmentById(int id)
        {
            return await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Assignment> UpdateAssignment(int assignmentId,Assignment assignment,string trainerId)
        {
            var existingAssignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id ==assignmentId  && a.TrainerId == trainerId);
            if (existingAssignment is null)
            {
                return null;
            }
            existingAssignment.Title = assignment.Title;
            existingAssignment.Description = assignment.Description;
            existingAssignment.DueDate = assignment.DueDate;
            existingAssignment.CourseId = assignment.CourseId;
            existingAssignment.TotalMarks = assignment.TotalMarks;
            existingAssignment.ModuleId = assignment.ModuleId;
            existingAssignment.TrainerId = trainerId;
            await _context.SaveChangesAsync();
            return existingAssignment;
        }
    }
}
