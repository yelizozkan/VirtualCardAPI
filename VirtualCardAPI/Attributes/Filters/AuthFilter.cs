using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VirtualCardAPI.Services.Abstract;

namespace VirtualCardAPI.Attributes.Filters
{
    public class AuthFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.User.Identity?.IsAuthenticated ?? false;
            if (!isAuthenticated)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult(); // 401 Unauthorized
            }

            await Task.CompletedTask;
        }
    }
}
