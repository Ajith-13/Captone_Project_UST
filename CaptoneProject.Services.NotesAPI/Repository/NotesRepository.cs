using CaptoneProject.Services.NotesAPI.Data;
using CaptoneProject.Services.NotesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptoneProject.Services.NotesAPI.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly AppDbContext _context;

        public NotesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notes>> GetAllNotes()
        {
            //while;dbfdnc
            return await _context.Notes.ToListAsync();
        }
        public async Task<IEnumerable<Notes>> GetNotesByUser(int userId)
        {
            return await _context.Notes
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.DateModified)
            .ToListAsync();
        }

        


        

        public async Task<Notes> CreateNote(Notes notes, int userid)
        {
            Notes n = new Notes()
            {
                UserId = userid,
                Title = notes.Title,
                Description = notes.Description,
                DateCreated = notes.DateCreated,
                DateModified = notes.DateModified,
                Resources = notes.Resources,
            };
            _context.Notes.Add(n);
            await _context.SaveChangesAsync();
            return n;
        }

        public async Task<bool> UpdateNote(int userid, Notes notes)
        {
            var n = _context.Notes.FirstOrDefault(n => n.UserId == userid);
            if (n == null)
            {
                return false;
            }
            n.Title = notes.Title;
            n.Description = notes.Description;
            n.DateCreated = notes.DateCreated;
            n.DateModified = notes.DateModified;
            n.Resources = notes.Resources;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNote(int Id)
        {
            var notes = await _context.Notes.FindAsync(Id);
            if (notes == null)
            {
                return false;
            }

            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
