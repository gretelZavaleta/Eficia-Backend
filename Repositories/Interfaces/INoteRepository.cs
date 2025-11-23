using EficiaBackend.Models;

namespace EficiaBackend.Repositories.Interfaces
{
    public interface INoteRepository
    {
       
        public Task<IEnumerable<Note>> GetNotesAsync();
        public Task<Note?> GetNoteByIdAsync(int id);
        public Task<Note> CreateNoteAsync(Note note);
        public Task<Note> UpdateNoteAsync(Note note);
        public Task<bool> DeleteNoteAsync(int id);

    }
}
