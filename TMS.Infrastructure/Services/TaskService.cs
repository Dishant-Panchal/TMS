using TMS.Domain.Models;
using TMS.Infrastructure.Repositories;

namespace TMS.Infrastructure.Services
{
    public class TaskService(ITaskRepository taskRepository) : ITaskService
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<IEnumerable<EmployeeTask>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<EmployeeTask> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task AddTaskAsync(EmployeeTask task)
        {
            await _taskRepository.AddAsync(task);
        }

        public async Task UpdateTaskAsync(EmployeeTask task)
        {
            await _taskRepository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task CompleteAsync(int id, bool isCmplt)
        {
            await _taskRepository.CompleteAsync(id, isCmplt);
        }

        public async Task<IEnumerable<EmployeeTask>> GetTeamTasksAsync(int managerId)
        {
            return await _taskRepository.GetTeamTasksAsync(managerId);
        }
    }

    public interface ITaskService
    {
        Task<IEnumerable<EmployeeTask>> GetAllTasksAsync();
        Task<EmployeeTask> GetTaskByIdAsync(int id);
        Task AddTaskAsync(EmployeeTask task);
        Task UpdateTaskAsync(EmployeeTask task);
        Task DeleteTaskAsync(int id);
        Task CompleteAsync(int id, bool isCmplt);
        Task<IEnumerable<EmployeeTask>> GetTeamTasksAsync(int managerId);
    }
}
