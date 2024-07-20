using TMS.Domain.Models;
using TMS.Infrastructure.Repositories;

namespace TMS.Infrastructure.Services
{
    public class TaskAttachmentService(ITaskAttachmentRepository taskAttachmentRepository) : ITaskAttachmentService
    {
        private readonly ITaskAttachmentRepository _taskAttachmentRepository = taskAttachmentRepository;

        public async Task<IEnumerable<TaskAttachment>> GetByIdAsync(int taskId)
        {
            return await _taskAttachmentRepository.GetByIdAsync(taskId);
        }

        public async Task AddAsync(TaskAttachment attachment)
        {
            await _taskAttachmentRepository.AddAsync(attachment);
        }

        public async Task DeleteAsync(int id)
        {
            await _taskAttachmentRepository.DeleteAsync(id);
        }
    }

    public interface ITaskAttachmentService
    {
        Task AddAsync(TaskAttachment attachment);
        Task DeleteAsync(int id);
        Task<IEnumerable<TaskAttachment>> GetByIdAsync(int taskId);
    }
}
