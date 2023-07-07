using AccountManager.Api.Exceptions;
using AccountManager.Api.Interfaces;
using AccountManager.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace AccountManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] UserCreateRequest request)
        {
            try
            {
                var result = await _userService.CreateAsync(request);

                return Ok(result);
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Create]: Unexpected error.");

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{uuid}")]
        public async Task<ActionResult<ValidationResult>> Put([FromRoute] Guid uuid, [FromBody] UserUpdateRequest request)
        {
            try
            {
                var result = await _userService.UpdateAsync(uuid, request);

                return Ok(new ValidationResult());
            }
            catch (UserAttributeException ex)
            {
                var validationResult = new ValidationResult()
                {
                    Errors = new()
                    {
                        new Contract.Models.ValidationError(ex.Code, ex.Message, ex.Attribute),
                    },
                };
                return Ok(validationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Create]: Unexpected error.");

                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var result = await _userService.GetAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("{uuid}")]
        public async Task<ActionResult<User>> Get([FromRoute] Guid uuid)
        {
            try
            {
                var result = await _userService.GetAsync(uuid);

                return Ok(result);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]: Unexpected error.");

                return BadRequest();
            }
        }
    }
}
