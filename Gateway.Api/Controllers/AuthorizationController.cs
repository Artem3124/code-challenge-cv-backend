using Microsoft.AspNetCore.Mvc;
using Gateway.Contact.Models;
using AccountManager.Contract.Models;
using AccountManager.Contract;
using Shared.Core.Extensions;
using Shared.Core.Exceptions;
using Shared.Core.Constants;
using Gateway.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAccountManagerClient _accountManagerClient;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IGatewayAuthorizationService _authorizationService;

        public AuthorizationController(IAccountManagerClient accountManagerClient, IGatewayAuthorizationService authorizationService, ILogger<AuthorizationController> logger)
        {
            _accountManagerClient = accountManagerClient.ThrowIfNull();
            _authorizationService = authorizationService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] AuthorizationRequest request)
        {
            User user;
            try
            {
                user = await _accountManagerClient.LoginAsync(new LoginRequest(request.Email, request.Password));
            }
            catch (ClientUnexpectedErrorException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Unauthorized(new GatewayError(Errors.CredentialsInvalid));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Login]: Unexpected error.");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }

            await _authorizationService.SingInAsync(user, HttpContext, new()
            {
                IsPersistent = request.RememberMe,
            });

            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequest request)
        {
            try
            {
                await _accountManagerClient.RegisterAsync(new UserCreateRequest(request.Email, request.Password, request.Login, request.RepeatPassword));

                return Ok();
            }
            catch (ClientUnexpectedErrorException ex) when (ex.Content.Contains(Errors.EmailInvalid.Code))
            {
                return BadRequest(new GatewayError(Errors.EmailInvalid));
            }
            catch (ClientUnexpectedErrorException ex) when (ex.Content.Contains(Errors.PasswordLengthInvalid.Code))
            {
                return BadRequest(new GatewayError(Errors.PasswordLengthInvalid));
            }
            catch (ClientUnexpectedErrorException ex) when (ex.Content.Contains(Errors.LoginInvalid.Code))
            {
                return BadRequest(new GatewayError(Errors.LoginInvalid));
            }
            catch (ClientUnexpectedErrorException ex) when (ex.Content.Contains(Errors.UserExists.Code))
            {
                return Unauthorized(new GatewayError(Errors.UserExists));
            }
            catch (ClientUnexpectedErrorException ex) when (ex.Content.Contains(Errors.PasswordMissmatch.Code))
            {
                return BadRequest(new GatewayError(Errors.PasswordMissmatch));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Register]: Unexpected error.");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            try
            {
                var user = _authorizationService.GetUser(HttpContext.User);
                if (user == null || user.UUID == Guid.Empty)
                {
                    return Unauthorized();
                }

                var result = await _accountManagerClient.Get(user.UUID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]: Unexpected error.");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            await _authorizationService.SingOutAsync(HttpContext);

            return NoContent();
        }
    }
}
