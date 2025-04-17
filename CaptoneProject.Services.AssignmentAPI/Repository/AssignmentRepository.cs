using CaptoneProject.Services.AssignmentAPI.Data;
using CaptoneProject.Services.AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        public readonly AppDbContext _context;
        public AssignmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Assignment> AddAssignment(Assignment assignment)
        {
            await _context.Assignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        //public async Task<bool> DeleteAssignment(string trainerId, int assignmentId)
        //{
        //    var assignment=await _context.Assignments.FirstOrDefaultAsync(a=>a.Id==assignmentId);
        //    if (assignment != null)
        //    {
        //       _context.Assignments.Remove(assignment);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        public async Task<IEnumerable<Assignment>> GetAllAssignment()
        {
            return await _context.Assignments.ToListAsync();
        }

        public async Task<Assignment> GetAssignmentById(int id)
        {
            return await _context.Assignments.FindAsync(id);
        }
        public async Task<Assignment> UpdateMark(Assignment assignment)
        {
            _context.Entry(assignment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return assignment;
        }
    }
}
