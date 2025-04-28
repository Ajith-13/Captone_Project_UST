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
            return await _context.Notes.ToListAsync();
        }

        public async Task<IEnumerable<Notes>> GetNotesByUser(string userId)
        {
            return await _context.Notes
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.DateModified)
                    .ToListAsync();
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

        public async Task<bool> UpdateNote(string userid, Notes notes)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == notes.Id && n.UserId == userid);
            if (existingNote == null) return false;

            existingNote.Title = notes.Title;
            existingNote.Description = notes.Description;
            existingNote.DateModified = notes.DateModified;
            existingNote.Resources = notes.Resources;

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

        // Fix for CS0535: Implementing the missing interface member
        public async Task<bool> DeleteNote(int Id, string userId)
        {
            var notes = await _context.Notes.FirstOrDefaultAsync(n => n.Id == Id && n.UserId == userId);
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
