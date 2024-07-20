using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Domain.Models;
using TMS.Infrastructure.Services;

namespace TMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<EmployeeTask>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTask>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult> AddTask(EmployeeTask task)
        {
            await _taskService.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, EmployeeTask task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            await _taskService.UpdateTaskAsync(task);
            return Ok("Task updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok("Task deleted successfully!");
        }

        [HttpPut("{id}/{isCmplt}")]
        public async Task<IActionResult> CompleteTask(int id, bool isCmplt)
        {
            await _taskService.CompleteAsync(id, isCmplt);
            string cmplt = isCmplt ? "Completed" : "InComplete";
            return Ok($"Task set to {cmplt}!");
        }

        [HttpGet("team/{managerId}")]
        public async Task<ActionResult<IEnumerable<EmployeeTask>>> GetTeamTasks(int managerId)
        {
            var tasks = await _taskService.GetTeamTasksAsync(managerId);
            return Ok(tasks);
        }
    }
}
