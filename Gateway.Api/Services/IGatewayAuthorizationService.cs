using AccountManager.Contract.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Gateway.Api.Services
{
    public interface IGatewayAuthorizationService
    {
        User GetUser(ClaimsPrincipal claimsPrincipal);
        Task SingInAsync(User user, HttpContext context, AuthenticationProperties? properties = null);
        Task SingOutAsync(HttpContext context);
    }
}