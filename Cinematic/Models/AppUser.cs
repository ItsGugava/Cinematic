using Microsoft.AspNetCore.Identity;

namespace Cinematic.Models
{
    public class AppUser : IdentityUser
    {
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
