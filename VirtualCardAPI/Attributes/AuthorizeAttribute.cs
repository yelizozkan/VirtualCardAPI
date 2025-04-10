using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VirtualCardAPI.Services.Abstract;

namespace VirtualCardAPI.Attributes
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute() : base(typeof(Filters.AuthFilter))
        {
        }
    }
}
