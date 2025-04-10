using System.Security.Claims;
using VirtualCardAPI.Services.Abstract;

namespace VirtualCardAPI.Services.Concrete
{
    public class FakeAuthService : IAuthService
    {
        public bool IsLoggedIn()
        {
            // Fake olarak her zaman giriş yapmış gibi dönüyor
            return true;
        }

        public string? GetUsername()
        {
            return "fakeuser"; 
        }

        public IEnumerable<string> GetRoles()
        {
            return new List<string> { "User", "Admin" }; 
        }
    }
}
