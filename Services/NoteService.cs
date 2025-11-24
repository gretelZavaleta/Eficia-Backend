using EficiaBackend.Dtos.Notes;
using EficiaBackend.Models;
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
            var notes = await _Repository.GetNotesAsync();
            return notes.Select(n => new NoteDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                IsArchived = n.IsArchived,
                CreatedAt = n.CreatedAt
            });

        }
        public async Task<NoteDto?> GetById(int id)
        {
            var note = await _Repository.GetNoteByIdAsync(id);
            return note == null
                ? null
                : new NoteDto
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    IsArchived = note.IsArchived,
                    CreatedAt = note.CreatedAt
                };



        }
        public async Task<NoteDto> Create(CreateNoteDto createNoteDto)
        {
            var newNote = new Note()
            {
                Title = createNoteDto.Title,
                Content = createNoteDto.Content,
                UserId = createNoteDto.UserId,
                IsArchived = false,
            }; 
           var saveNote =  await _Repository.CreateNoteAsync(newNote);
           var newNoteDto = new NoteDto()
           {
               Id = saveNote.Id,
               Title = saveNote.Title,
               Content = saveNote.Content,
               IsArchived = saveNote.IsArchived,
               CreatedAt = saveNote.CreatedAt
           };
            return newNoteDto;

        }

        public async Task<NoteDto?> Update(int id,UpdateNoteDto updateNoteDto)
        {
            var existingNote = await _Repository.GetNoteByIdAsync(id);

            if (existingNote is { })
            {
                if (updateNoteDto.Title != null) existingNote.Title = updateNoteDto.Title;
                if (updateNoteDto.Content != null) existingNote.Content = updateNoteDto.Content;
                if (updateNoteDto.IsArchived != null) existingNote.IsArchived = updateNoteDto.IsArchived.Value;

                existingNote.UpdatedAt = DateTime.UtcNow;

                await _Repository.UpdateNoteAsync(existingNote);

                return new NoteDto
                {
                    Id = existingNote.Id,
                    Title = existingNote.Title,
                    Content = existingNote.Content,
                    IsArchived = existingNote.IsArchived,
                    CreatedAt = existingNote.CreatedAt
                };
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            return await _Repository.DeleteNoteAsync(id);
        }


    }
}
