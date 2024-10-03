using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Netflix.API.Common.Helpers
{
    public static class ClientContextHelper
    {
        public static Guid GetClientId(HttpContext httpContext)
        {
            var claimsIdentity = httpContext?.User?.Identity as ClaimsIdentity;
            var strClientId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (strClientId is not null)
            {
                return new Guid(strClientId);
            }

            throw new Exception("Failed to parse user id from token");
        }
    }
}
