using System.Diagnostics;

namespace VirtualCardAPI.Extensions
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"Request to {context.Request.Method} {context.Request.Path} took {stopwatch.ElapsedMilliseconds}ms");

        }
    }
}
