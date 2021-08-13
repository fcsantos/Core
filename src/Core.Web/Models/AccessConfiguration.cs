using Core.Web.Extensions;
using System.Linq;

namespace Core.Web.Models
{
    public class AuthAccessConfiguration
    {
        public bool HasAccessTo(IAspNetUser user, string type, string value)
        {
            return user.GetClaims().Where(c => c.Value == value && c.Type == type).Any();
        }
    }
}
