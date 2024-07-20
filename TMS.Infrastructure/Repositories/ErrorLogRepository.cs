using TMS.Domain.Models;

namespace TMS.Infrastructure.Repositories
{
    public class ErrorLogRepository(TMSContext context) : IErrorLogRepository
    {
        private readonly TMSContext _context = context;

        public async Task AddAsync(ErrorLog log)
        {
            await _context.ErrorLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }

    public interface IErrorLogRepository
    {
        Task AddAsync(ErrorLog log);
    }
}
