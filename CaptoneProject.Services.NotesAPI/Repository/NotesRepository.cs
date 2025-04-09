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

        public async Task<IEnumerable<Notes>> GetNotesByUser(string userId)
        {
            return await _context.Notes
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.DateModified)
            .ToListAsync();
        }

        public async Task<List<Notes>> GetNoteByCreatedDate(DateTime dateCreated, string userid)
        {
            return await _context.Notes.Where(n => n.DateCreated.Date == dateCreated.Date && n.UserId == userid).ToListAsync();
        }


        public async Task<List<Notes>> GetNoteByModifiedDate(DateTime dateModified, string userid)
        {
            return await _context.Notes.Where(n => n.DateModified.Date == dateModified.Date && n.UserId == userid).ToListAsync();
        }

        public async Task<Notes> CreateNote(Notes notes, string userid)
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

        public async Task<bool> UpdateNote(int id, Notes notes)
        {
            var n = _context.Notes.FirstOrDefault(n => n.Id == id);
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

        public async Task<bool> DeleteNote(int id)
        {
            var notes = await _context.Notes.FindAsync(id);
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
