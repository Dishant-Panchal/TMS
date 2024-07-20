using System.Text;

namespace TMS.Api.Helpers
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionDetails(this Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var exceptionDetails = new StringBuilder();
            var currentException = exception;

            while (currentException != null)
            {
                exceptionDetails.AppendLine($"Exception Type: {currentException.GetType().FullName}");
                exceptionDetails.AppendLine($"Message: {currentException.Message}");
                exceptionDetails.AppendLine($"Stack Trace: {currentException.StackTrace}");

                var properties = currentException.GetType().GetProperties();

                foreach (var property in properties)
                {
                    // Exclude properties that are handled explicitly
                    if (property.Name == nameof(Exception.Message) ||
                        property.Name == nameof(Exception.StackTrace) ||
                        property.Name == nameof(Exception.InnerException))
                    {
                        continue;
                    }

                    var value = property.GetValue(currentException, null);
                    exceptionDetails.AppendLine($"{property.Name}: {value?.ToString() ?? "null"}");
                }

                currentException = currentException.InnerException;
                if (currentException != null)
                {
                    exceptionDetails.AppendLine("Inner Exception:");
                }
            }

            return exceptionDetails.ToString();
        }
    }
}
