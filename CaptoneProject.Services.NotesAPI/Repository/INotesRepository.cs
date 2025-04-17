using CaptoneProject.Services.NotesAPI.Models;

namespace CaptoneProject.Services.NotesAPI.Repository
{
    public interface INotesRepository
    {
        Task<IEnumerable<Notes>> GetAllNotes();
        Task<IEnumerable<Notes>> GetNotesByUser(string userId);
        Task<Notes> CreateNote(Notes notes, string userid);
        Task<bool> UpdateNote(string userid, Notes notes);
        Task<bool> DeleteNote(int Id);
    }
   
}
