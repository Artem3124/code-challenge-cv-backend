using AccountManager.Contract;
using AccountManager.Contract.Models;
using Gateway.Api.Services;
using Gateway.Contact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Constants;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IGatewayAuthorizationService _authorizationService;
        private readonly IAccountManagerClient _accountManagerClient;
        private readonly ILogger<UserController> _logger;

        public UserController(IGatewayAuthorizationService authorizationService, IAccountManagerClient accountManagerClient, ILogger<UserController> logger)
        {
            _authorizationService = authorizationService.ThrowIfNull();
            _accountManagerClient = accountManagerClient.ThrowIfNull();
            _logger = logger.ThrowIfNull();
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<List<User>>> ReadAll()
        {
            var result = await _accountManagerClient.Get();

            return Ok(result);
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> Read()
        {
            var result = await _accountManagerClient.Get(GetUser().UUID);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult<ValidationResult>> Patch(UserUpdateRequest request)
        {
            try
            {
                var result = await _accountManagerClient.UpdateUserAsync(GetUser().UUID, request);

                return Ok(result);
            }
            catch (ClientUnexpectedErrorException ex) when (ex.Content.Contains(Errors.PasswordUpdateMismatch.Code))
            {
                return BadRequest(new GatewayError(Errors.PasswordUpdateMismatch));
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
                _logger.LogError(ex, "[{method}]: Unexpected error.", nameof(Read));

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }

        [HttpGet("{userUUID}")]
        public async Task<ActionResult<UserShort>> Read(Guid userUUID)
        {
            var result = await _accountManagerClient.Get(userUUID);

            return Ok(new UserShort
            {
                Login = result.Login,
                UUID = result.UUID,
            });
        }

        private User GetUser() => _authorizationService.GetUser(User);
    }
}
