using EficiaBackend.Dtos.Notes;
using EficiaBackend.Repositories.Interfaces;
using EficiaBackend.Services.Interfaces;
using System.Linq;
namespace EficiaBackend.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _Repository;
        public NoteService(INoteRepository InoteRepository)
        {
            this._Repository = InoteRepository;
        }
        public async Task<IEnumerable<NoteDto>> GetAll()
        {
            await _Repository.GetNotesAsync();
                   
        }
        public Task<NoteDto?> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<NoteDto> Create(CreateNoteDto createNoteDto)
        {
            throw new NotImplementedException();
        }

        public Task<NoteDto?> Update(int id,UpdateNoteDto updateNoteDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }


    }
}
