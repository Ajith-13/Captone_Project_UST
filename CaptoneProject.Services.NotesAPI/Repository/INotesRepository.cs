using CaptoneProject.Services.NotesAPI.Models;

namespace CaptoneProject.Services.NotesAPI.Repository
{
    public interface INotesRepository
    {
        Task<IEnumerable<Notes>> GetNotesByUser(string userId);
        Task<List<Notes>> GetNoteByCreatedDate(DateTime dateCreated, string userid);
        Task<List<Notes>> GetNoteByModifiedDate(DateTime dateModified, string userid);
        Task<Notes> CreateNote(Notes notes, string userid);
        Task<bool> UpdateNote(int userid, Notes notes);
        Task<bool> DeleteNote(int id);
    }
   
}
