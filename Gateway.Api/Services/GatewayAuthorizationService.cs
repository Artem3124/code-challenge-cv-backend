using AccountManager.Contract.Models;
using AccountManager.Data.Enum;
using Microsoft.AspNetCore.Authentication;
using Shared.Core.Extensions;
using System.Security.Claims;

namespace Gateway.Api.Services
{
    public class GatewayAuthorizationService : IGatewayAuthorizationService
    {
        private readonly string _authenticationScheme;

        public GatewayAuthorizationService(string authenticationScheme)
        {
            _authenticationScheme = authenticationScheme;
        }

        public Task SingInAsync(User user, HttpContext context, AuthenticationProperties? properties = default)
        {
            properties ??= new AuthenticationProperties();
            return context.SignInAsync(_authenticationScheme, GetClaimsPrincipal(user), properties);
        }

        public Task SingOutAsync(HttpContext context)
        {
            return context.SignOutAsync(_authenticationScheme);
        }

        public User GetUser(ClaimsPrincipal claimsPrincipal)
        {
            var identity = claimsPrincipal.Identities.FirstOrDefault();
            if (identity == null)
            {
                throw new UnauthorizedAccessException();
            }

            var user = new User();
            var value = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(value) && Guid.TryParse(value, out var uuid))
            {
                user.UUID = uuid;
            }

            value = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(value) && Enum.TryParse(typeof(Role), value, out var role) && role != null)
            {
                user.Role = (Role)role;
            }

            value = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(value))
            {
                user.Email = value;
            }
            return user;
        }

        private ClaimsPrincipal GetClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UUID.ToString()),
                new Claim(ClaimTypes.Role, user.Role.GetDescription()),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var claimsIdentity = new ClaimsIdentity(claims, _authenticationScheme);

            return new(claimsIdentity);
        }
    }
}
