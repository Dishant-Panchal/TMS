using Microsoft.AspNetCore.Diagnostics;
using TMS.Domain.Models;
using TMS.Infrastructure.Services;

namespace TMS.Api.Helpers
{
    internal sealed class GlobalExceptionHandler(IErrorLogService logger) : IExceptionHandler
    {
        private readonly IErrorLogService _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ErrorLog log = new()
            {
                Exception = exception.GetExceptionDetails(),
                StackTrace = exception.StackTrace,
                ErrorMessage = exception.Message
            };

            await _logger.AddAsync(log);

            return true;
        }
    }
}
