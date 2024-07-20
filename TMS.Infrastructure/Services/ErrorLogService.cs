using TMS.Domain.Models;
using TMS.Infrastructure.Repositories;

namespace TMS.Infrastructure.Services
{
    public class ErrorLogService(IErrorLogRepository errorLogRepository) : IErrorLogService
    {
        private readonly IErrorLogRepository _errorLogRepository = errorLogRepository;

        public async Task AddAsync(ErrorLog log)
        {
            await _errorLogRepository.AddAsync(log);
        }
    }

    public interface IErrorLogService
    {
        Task AddAsync(ErrorLog log);
    }
}
