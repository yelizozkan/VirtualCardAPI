namespace VirtualCardAPI.Services.Abstract
{
    public interface IAuthService
    {
        bool IsLoggedIn();
        string? GetUsername();
        IEnumerable<string>? GetRoles();

    }
}
