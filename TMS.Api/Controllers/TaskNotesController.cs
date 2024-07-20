using Microsoft.AspNetCore.Mvc;
using TMS.Domain.Models;
using TMS.Infrastructure.Services;

namespace TMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskNotesController(ITaskNotesService taskNotesService) : ControllerBase
    {
        private readonly ITaskNotesService _taskNotesService = taskNotesService;

        [HttpGet("get-task-notes/{taskId}")]
        public async Task<ActionResult<TaskNote>> GetByTaskId(int taskId)
        {
            var task = await _taskNotesService.GetByTaskIdAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult> AddTask(TaskNote taskNote)
        {
            await _taskNotesService.AddAsync(taskNote);
            return CreatedAtAction(nameof(GetByTaskId), new { id = taskNote.Id }, taskNote);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskNote taskNote)
        {
            if (id != taskNote.Id)
            {
                return BadRequest();
            }
            await _taskNotesService.UpdateAsync(taskNote);
            return Ok("Task Note updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskNotesService.DeleteAsync(id);
            return Ok("Task Note deleted successfully!");
        }
    }
}
