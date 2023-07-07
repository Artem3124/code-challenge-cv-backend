using AccountManager.Contract;
using AccountManager.Contract.Models;
using Gateway.Api.Services;

namespace Gateway.Api.Middleware
{
    public class AuthenticationFakeMiddleware
    {
        private readonly RequestDelegate _next;
        private string _email = "fake@email.xom";
        private string _password = "password";
        private string _login = "loginlogin";

        public AuthenticationFakeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAccountManagerClient accountManagerClient,
            IGatewayAuthorizationService authorizationService,
            ILogger<AuthenticationFakeMiddleware> logger)
        {
            if (context.User.Identities.Any(i => i.IsAuthenticated))
            {
                await _next(context);

                return;
            }

            var user = await GetUserOrNull(() => accountManagerClient.LoginAsync(new LoginRequest(_email, _password)))
                ?? await GetUserOrNull(() => accountManagerClient.RegisterAsync(new UserCreateRequest(_email, _password, _login, _password)));
            if (user == null)
            {
                logger.LogWarning("Unexpected error ocurred while trying to fake user.");

                await _next(context);

                return;
            }

            await authorizationService.SingInAsync(user, context);

            await _next(context);
        }

        private async Task<User?> GetUserOrNull(Func<Task<User>> func)
        {
            try
            {
                var user = await func();

                return user;
            }
            catch
            {
                return null;
            }
        }
    }

    public static class AuthenticationFakeMiddlewareExtensions
    {
        public static IApplicationBuilder UseFakeAuthentication(this IApplicationBuilder builder) =>
            builder.UseMiddleware<AuthenticationFakeMiddleware>();
    }

}
