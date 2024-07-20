using Microsoft.EntityFrameworkCore;
using TMS.Domain.Models;

namespace TMS.Infrastructure.Repositories
{
    public class TaskNotesRepository : ITaskNotesRepository
    {
        private readonly TMSContext _context;
        public TaskNotesRepository(TMSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskNote>> GetByTaskIdAsync(int taskId)
        {
            var taskNotes = await _context.TaskNotes.Where(x => x.TaskId == taskId).ToListAsync();
            taskNotes.AsQueryable();
            return taskNotes;
        }

        public async Task AddAsync(TaskNote taskNote)
        {
            await _context.TaskNotes.AddAsync(taskNote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskNote taskNote)
        {
            _context.TaskNotes.Update(taskNote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.TaskNotes.FindAsync(id);
            if (task != null)
            {
                _context.TaskNotes.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface ITaskNotesRepository
    {
        Task<IEnumerable<TaskNote>> GetByTaskIdAsync(int taskId);
        Task AddAsync(TaskNote taskNote);
        Task UpdateAsync(TaskNote taskNote);
        Task DeleteAsync(int id);
    }
}
