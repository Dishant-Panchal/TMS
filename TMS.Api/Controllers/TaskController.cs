using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMS.Domain.Models;
using TMS.Domain.ViewModels;
using TMS.Infrastructure.Services;

namespace TMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITaskService taskService, IMapper mapper, IConfiguration configuration, ITaskAttachmentService taskAttachmentService) : Controller
    {
        private readonly ITaskService _taskService = taskService;
        private readonly ITaskAttachmentService _taskAttachmentService = taskAttachmentService;
        private readonly IMapper _mapper = mapper;
        public IConfiguration _configuration = configuration;

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<TaskVM>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            var taskVM = _mapper.Map<IEnumerable<TaskVM>>(tasks);
            taskVM = taskVM.Zip(tasks, (vm, task) =>
            {
                vm.EmployeeName = string.Format("{0} {1}", task.Employee?.FirstName, task.Employee?.LastName);
                vm.Notes = string.Join(",", task.TaskNotes.Select(x => x.Note));
                vm.File = string.Join(",", task.TaskAttachments.Select(x => x.FileName));
                return vm;
            }).ToList();
            return Json(taskVM);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskVM>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            var taskVM = _mapper.Map<TaskVM>(task);
            taskVM.EmployeeName = string.Format("{0} {1}", task.Employee?.FirstName, task.Employee?.LastName);
            taskVM.Notes = string.Join(",", task.TaskNotes.Select(x => x.Note));
            taskVM.File = string.Join(",", task.TaskAttachments.Select(x => x.FileName));
            return Json(taskVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskVM taskVM)
        {
            var task = _mapper.Map<EmployeeTask>(taskVM);
            await _taskService.AddTaskAsync(task);
            await SaveTaskAttachment(task.Id, taskVM);
            return Ok("Task added successfully!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(TaskVM taskVM)
        {
            var task = _mapper.Map<EmployeeTask>(taskVM);
            await _taskService.UpdateTaskAsync(task);
            await SaveTaskAttachment(taskVM.Id.Value, taskVM);
            return Ok("Task updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok("Task deleted successfully!");
        }

        /// <summary>
        /// This API helps to set task's status either Complete or InComplete
        /// </summary>
        /// <param name="id">taskId</param>
        /// <param name="isCmplt">true or false</param>
        /// <returns></returns>
        [HttpPut("{id}/{isCmplt}")]
        public async Task<IActionResult> CompleteTask(int id, bool isCmplt)
        {
            await _taskService.CompleteAsync(id, isCmplt);
            string cmplt = isCmplt ? "Completed" : "InComplete";
            return Ok($"Task set to {cmplt}!");
        }

        /// <summary>
        /// This API helps Employee Manager or Team Lead to track their team member's tasks
        /// </summary>
        /// <param name="managerId">Pass manager Id or Team Lead employee Id</param>
        /// <returns>List of tasks of their Team members</returns>
        [HttpGet("team/{managerId}")]
        public async Task<ActionResult<IEnumerable<TaskVM>>> GetTeamTasks(int managerId)
        {
            var tasks = await _taskService.GetTeamTasksAsync(managerId);
            var taskVM = _mapper.Map<IEnumerable<TaskVM>>(tasks);
            taskVM = taskVM.Zip(tasks, (vm, task) =>
            {
                vm.EmployeeName = string.Format("{0} {1}", task.Employee?.FirstName, task.Employee?.LastName);
                vm.Notes = string.Join(",", task.TaskNotes.Select(x => x.Note));
                vm.File = string.Join(",", task.TaskAttachments.Select(x => x.FileName));
                return vm;
            }).ToList();
            return Json(taskVM);
        }

        [HttpGet("/test")]
        public IActionResult Test()
        {
            throw new NotImplementedException();
        }

        private async Task SaveTaskAttachment(int taskId, TaskVM taskVM)
        {
            try
            {
                if (taskVM.Attachment is not null && taskVM.Attachment.Length > 0)
                {
                    var file = taskVM.Attachment;
                    var fName = file.FileName;
                    var ext = Path.GetExtension(fName).ToLower();
                    string folderPath;
                    if (!string.IsNullOrEmpty(fName))
                    {
                        folderPath = Path.Combine(Directory.GetCurrentDirectory() + string.Format(Convert.ToString(_configuration.GetSection("FolderPath").Value), taskId));
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        string filePath = Path.Combine(folderPath, fName);
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                        using var stream = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(stream);
                    }

                    TaskAttachment taskAttachment = new();
                    taskAttachment.FileName = fName;
                    taskAttachment.FileExtension = ext;
                    taskAttachment.TaskId = taskId;
                    await _taskAttachmentService.AddAsync(taskAttachment);
                }
            }
            catch (Exception ex) { }
        }
    }
}
