using AccountManager.Contract.Models;
using CodeRunManager.Contract.Models;
using Gateway.Api.Interfaces;
using Gateway.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Constants;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CodeRunResultController : ControllerBase
    {
        private readonly IGatewayAuthorizationService _authorizationService;
        private readonly ICodeRunResultService _codeRunResultService;
        private readonly ILogger<CodeRunResultController> _logger;

        public CodeRunResultController(IGatewayAuthorizationService authorizationService, ICodeRunResultService codeRunResultService, ILogger<CodeRunResultController> logger)
        {
            _authorizationService = authorizationService.ThrowIfNull();
            _codeRunResultService = codeRunResultService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
        }

        [HttpGet]
        public async Task<ActionResult<List<CodeRunResultExpanded>>> Get()
        {
            var user = GetUser();

            try
            {
                var result = await _codeRunResultService.GetByUserUUIDAsync(user.UUID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]: Unexpected error.");

                return BadRequest(Errors.UnexpectedError);
            }
        }

        [HttpGet]
        [Route("codeProblem/{codeProblemUUID}")]
        public async Task<ActionResult<List<CodeRunResultExpanded>>> GetByCodeProblemUUID([FromRoute] Guid codeProblemUUID)
        {
            var user = GetUser();

            try
            {
                var result = await _codeRunResultService.GetByCodeProblemUUIDAsync(user.UUID, codeProblemUUID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetByCodeProblemUUID]: Unexpected error while requesting runs for code problem {codeProblemUUID}. User {userUUID}.", codeProblemUUID, user?.UUID);

                return BadRequest(Errors.UnexpectedError);
            }
        }

        [HttpGet]
        [Route("{uuid}")]
        public async Task<ActionResult<CodeRunResultExpanded>> Get([FromRoute] Guid uuid)
        {
            var user = GetUser();

            try
            {
                var result = await _codeRunResultService.GetByUUIDAsync(user.UUID, uuid);
                
                return Ok(result);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound(uuid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]: Unexpected error while requesting run {uuid}. User {userUUID}.", uuid, user?.UUID);

                return BadRequest(Errors.UnexpectedError);
            }
        }

        private User GetUser() => _authorizationService.GetUser(HttpContext.User);
    }
}
