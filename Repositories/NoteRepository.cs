using EficiaBackend.Data;
using EficiaBackend.Models;
using EficiaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EficiaBackend.Repositories
{
    public class NoteRepository: INoteRepository
    {
        private readonly AppDbContext _context;
        public NoteRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Note>> GetNotesAsync()
        {
           return await _context.Notes.ToListAsync();

        }
        public async Task<Note?> GetNoteByIdAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return note;

        }
        public async Task<Note> UpdateNoteAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return note;


        }

        public async Task<bool> DeleteNoteAsync(int id)
        {
           var noteDelete =  await _context.Notes.FindAsync(id);
            if (noteDelete == null)
            {
                return false;
            }
            _context.Notes.Remove(noteDelete);
            await _context.SaveChangesAsync();
            return true;

        }

       

        

      
    }





}
