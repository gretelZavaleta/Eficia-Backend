using EficiaBackend.Dtos.Notes;
using EficiaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EficiaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> Index()
        {
            var notes = await _noteService.GetAll();
            return Ok(notes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> Show(int id)
        {
            var note = await _noteService.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }
        [HttpPost]
        public async Task<ActionResult<NoteDto>> Store(CreateNoteDto createNoteDto)
        {
            var createdNote = await _noteService.Create(createNoteDto);
            return CreatedAtAction(nameof(Show), new { id = createdNote.Id }, createdNote);
        }
    }
}
