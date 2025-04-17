using CaptoneProject.Services.AssignmentAPI.Data;
using CaptoneProject.Services.AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.AssignmentAPI.Repository
{
    public class AssignmentQuestionRepository : IAssignmentQuestionRepository
    {
        private readonly AppDbContext _context;
        public AssignmentQuestionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AssignmentQuestion> AddAssignmentQuestion(AssignmentQuestion assignmentQuestion)
        {
            await _context.AssignmentQuestions.AddAsync(assignmentQuestion);
            try
            {
            await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(innerMessage);
            }
            return assignmentQuestion;
        }

        public async Task<bool> DeleteAssignmentQuestion(string trainerId,int assignmentId)
        {
            var assignmentQuestion = await _context.AssignmentQuestions.FirstOrDefaultAsync(a=>a.Id==assignmentId && a.TrainerId==trainerId);
            if (assignmentQuestion is null)
            {
                return false;
            }
            _context.AssignmentQuestions.Remove(assignmentQuestion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AssignmentQuestion>> GetAllAssignmentQuestion()
        {
            return await _context.AssignmentQuestions.ToListAsync();

        }

        public async Task<AssignmentQuestion> GetAssignmentQuestionById(int id)
        {
            return await _context.AssignmentQuestions.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AssignmentQuestion> UpdateAssignmentQuestion(int assignmentId, AssignmentQuestion assignment,string trainerId)
        {
            var existingQuestion = await _context.AssignmentQuestions.FirstOrDefaultAsync(a => a.Id ==assignmentId  && a.TrainerId == trainerId);
            if (existingQuestion is null)
            {
                return null;
            }
            existingQuestion.Title = assignment.Title;
            existingQuestion.Description = assignment.Description;
            existingQuestion.DueDate = assignment.DueDate;
            existingQuestion.CourseId = assignment.CourseId;
            existingQuestion.TotalMarks = assignment.TotalMarks;
            existingQuestion.ModuleId = assignment.ModuleId;
            existingQuestion.TrainerId = trainerId;
            await _context.SaveChangesAsync();
            return existingQuestion;
        }
    }
}
