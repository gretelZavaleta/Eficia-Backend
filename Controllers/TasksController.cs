using Microsoft.AspNetCore.Mvc;
using EficiaBackend.Services.Interfaces;
using EficiaBackend.Dtos.Tasks;
using EficiaBackend.Models;

namespace EficiaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> Show(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        [HttpPost]
        public async Task<ActionResult<TaskDto>> Store(CreateTaskDto createTaskDto)
        {
            //PENDIENTE DE REVISIÓN
            // TODO: Obtener userId del usuario autenticado
            var userId = 1; // Temporal - reemplazar con autenticación real
            
            var createdTask = await _taskService.CreateTaskAsync(createTaskDto, userId);
            return CreatedAtAction(nameof(Show), new { id = createdTask.Id }, createdTask);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<TaskDto>> Update(int id, UpdateTaskDto updateTaskDto) // Cambiado a updateTaskDto
        {
            var updatedTask = await _taskService.UpdateTaskAsync(id, updateTaskDto);
            if (updatedTask == null)
            {
                return NotFound();
            }
            return Ok(updatedTask);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}