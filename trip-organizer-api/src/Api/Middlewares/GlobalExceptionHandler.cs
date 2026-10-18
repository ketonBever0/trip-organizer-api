using Microsoft.AspNetCore.Diagnostics;

namespace trip_organizer_api.src.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception");

            httpContext.Response.StatusCode = 500;

            await httpContext.Response.WriteAsJsonAsync(
                new
                {
                    message = "Internal error"
                },
                cancellationToken);

            return true;
        }
    }
}
