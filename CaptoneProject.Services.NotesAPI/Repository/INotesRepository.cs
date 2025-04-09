using CaptoneProject.Services.NotesAPI.Models;

namespace CaptoneProject.Services.NotesAPI.Repository
{
    public interface INotesRepository
    {
        Task<IEnumerable<Notes>> GetAllNotes();
        Task<IEnumerable<Notes>> GetNotesByUser(int userId);
        Task<Notes> CreateNote(Notes notes, int userid);
        Task<bool> UpdateNote(int userid, Notes notes);
        Task<bool> DeleteNote(int Id);
    }
   
}
