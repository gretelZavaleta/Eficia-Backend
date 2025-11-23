using EficiaBackend.Dtos.Notes;

namespace EficiaBackend.Services.Interfaces
{
    public interface INoteService
    {
        public Task<IEnumerable<NoteDto>> GetAll();
        public Task<NoteDto?> GetById(int id);
        public Task<NoteDto> Create(CreateNoteDto createNoteDto);
        public Task<NoteDto?> Update(int id, UpdateNoteDto updateNoteDto);
        public Task<bool> Delete(int id);
    }
}
