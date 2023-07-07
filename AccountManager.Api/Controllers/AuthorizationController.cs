using AccountManager.Api.Exceptions;
using AccountManager.Api.Interfaces;
using AccountManager.Contract.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Extensions;

namespace AccountManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IUserService _userService;
        public AuthorizationController(IAuthorizationService authorizationService, ILogger<AuthorizationController> logger, IUserService userService)
        {
            _authorizationService = authorizationService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
            _userService = userService.ThrowIfNull();
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserCreateRequest request)
        {
            try
            {
                var result = await _userService.CreateAsync(request);

                return Ok(result);
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserAttributeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Register]: Unexpected error.");

                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authorizationService.LoginAsync(request.Email, request.Password);

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Login]: Unexpected error.");

                return BadRequest();
            }
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return NoContent();
        }
    }
}
