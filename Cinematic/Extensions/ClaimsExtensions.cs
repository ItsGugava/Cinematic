using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cinematic.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {

            string userName = user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))?.Value;
            return userName;
        }


        public static string GetId(this ClaimsPrincipal user)
        {
            string id = user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            return id;
        }
    }
}
