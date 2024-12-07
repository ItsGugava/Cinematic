using Cinematic.Models;

namespace Cinematic.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
