using System.Linq;
using System.Security.Claims;

namespace Vib17
{
    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
