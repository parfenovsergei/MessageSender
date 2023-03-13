using MessageSenderAPI.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace MessageSenderAPI.Domain.Helpers
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] allowedRoles) 
        {
            var allowedRolesAsString = allowedRoles.Select(x => Enum.GetName(typeof(Role), x));
            Roles = string.Join(",", allowedRolesAsString);
        }
    }
}
