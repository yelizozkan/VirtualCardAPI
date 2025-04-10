using System.Net;

namespace VirtualCardAPI.Extensions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred."); 
                await HandleExceptionAsync(context);
            }

        }

        private static Task HandleExceptionAsync(HttpContext context)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            var errorResponse = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred on the server side."
            };


            return context.Response.WriteAsync(errorResponse.ToString());

        }

       
    }
}
