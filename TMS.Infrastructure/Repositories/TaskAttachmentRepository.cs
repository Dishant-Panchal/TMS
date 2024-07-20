using Microsoft.EntityFrameworkCore;
using TMS.Domain.Models;

namespace TMS.Infrastructure.Repositories
{
    public class TaskAttachmentRepository(TMSContext context) : ITaskAttachmentRepository
    {
        private readonly TMSContext _context = context;

        public async Task<IEnumerable<TaskAttachment>> GetByIdAsync(int taskId)
        {
            return await _context.TaskAttachments.Where(x => x.TaskId == taskId).ToListAsync();
        }

        public async Task AddAsync(TaskAttachment attachment)
        {
            await _context.TaskAttachments.AddAsync(attachment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var attachment = await _context.TaskAttachments.FindAsync(id);
            if (attachment != null)
            {
                _context.TaskAttachments.Remove(attachment);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface ITaskAttachmentRepository
    {
        Task AddAsync(TaskAttachment attachment);
        Task DeleteAsync(int id);
        Task<IEnumerable<TaskAttachment>> GetByIdAsync(int taskId);
    }
}
