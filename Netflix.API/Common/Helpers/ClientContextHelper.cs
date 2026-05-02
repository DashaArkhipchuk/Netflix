using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Common.Errors;
using System.Security.Claims;

namespace Netflix.API.Common.Helpers
{
    public static class ClientContextHelper
    {
        public static Guid GetClientId(HttpContext httpContext)
        {
            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext), "HttpContext is null.");

            var claimsIdentity = httpContext.User?.Identity as ClaimsIdentity;

            if (claimsIdentity is null)
                throw new AuthenticationException("No claims identity found. The request may be unauthenticated.");

            if (!claimsIdentity.IsAuthenticated)
            {
                // Check for common token expiry/invalidity indicators
                var authFailReason = claimsIdentity.FindFirst("error_description")?.Value
                                  ?? claimsIdentity.FindFirst("error")?.Value;

                if (authFailReason is not null)
                {
                    if (authFailReason.Contains("expired", StringComparison.OrdinalIgnoreCase))
                        throw new AuthenticationException($"Token has expired: {authFailReason}");

                    if (authFailReason.Contains("invalid", StringComparison.OrdinalIgnoreCase))
                        throw new AuthenticationException($"Token is invalid: {authFailReason}");
                }

                throw new AuthenticationException("Claims identity is not authenticated. Token may be expired or missing.");
            }

            var strClientId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (strClientId is null)
                throw new AuthenticationException($"Required claim '{ClaimTypes.NameIdentifier}' is missing from the token.");

            if (!Guid.TryParse(strClientId, out var clientId))
                throw new AuthenticationException($"Claim '{ClaimTypes.NameIdentifier}' value '{strClientId}' is not a valid GUID.");

            return clientId;
        }
    }
}
