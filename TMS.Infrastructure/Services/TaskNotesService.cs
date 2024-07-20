using TMS.Domain.Models;
using TMS.Infrastructure.Repositories;

namespace TMS.Infrastructure.Services
{
    public class TaskNotesService : ITaskNotesService
    {
        private readonly ITaskNotesRepository _taskNotesRepository;

        public TaskNotesService(ITaskNotesRepository taskNotesRepository)
        {
            _taskNotesRepository = taskNotesRepository;
        }

        public Task<IEnumerable<TaskNote>> GetByTaskIdAsync(int taskId)
        {
            return _taskNotesRepository.GetByTaskIdAsync(taskId);
        }

        public async Task AddAsync(TaskNote taskNote)
        {
            await _taskNotesRepository.AddAsync(taskNote);
        }

        public async Task DeleteAsync(int id)
        {
            await _taskNotesRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(TaskNote taskNote)
        {
            await _taskNotesRepository.UpdateAsync(taskNote);
        }
    }

    public interface ITaskNotesService
    {
        Task<IEnumerable<TaskNote>> GetByTaskIdAsync(int taskId);
        Task AddAsync(TaskNote taskNote);
        Task UpdateAsync(TaskNote taskNote);
        Task DeleteAsync(int id);
    }
}
