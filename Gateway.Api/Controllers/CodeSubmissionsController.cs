using Gateway.Api.Interfaces;
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
    public class CodeSubmissionsController : ControllerBase
    {
        private readonly ICodeSubmitService _codeSubmitService;
        private readonly ILogger<CodeSubmissionsController> _logger;
        private readonly IGatewayAuthorizationService _authorizationService;

        public CodeSubmissionsController(ICodeSubmitService codeSubmitService, IGatewayAuthorizationService authorizationService, ILogger<CodeSubmissionsController> logger)
        {
            _codeSubmitService = codeSubmitService.ThrowIfNull();
            _authorizationService = authorizationService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
        }

        [Authorize]
        [HttpPost("challenge")]
        public async Task<ActionResult<Guid>> CreateForChallenge([FromBody] CodeSubmitRequest request)
        {
            var user = _authorizationService.GetUser(HttpContext.User);
            try
            {
                var result = await _codeSubmitService.SubmitChallenge(request, user.UUID);

                return Ok(result);
            }
            catch (NotSupportedException)
            {
                return BadRequest(new GatewayError(Errors.CodeLanguageNotSupported));
            }
            catch (ClientUnexpectedErrorException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogError("[Create]: Code problem not found.");

                return NotFound(Errors.CodeProblemNotFound.Message);
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError("[Create]: {entityName} with id {entityUUID} was not found", ex.EntityName, ex.EntityUUID);

                return NotFound(new GatewayError(Errors.CodeProblemNotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Create]: Unexpected error occurred while submiting code");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CodeSubmitRequest request)
        {
            var user = _authorizationService.GetUser(HttpContext.User);
            try
            {
                var result = await _codeSubmitService.Submit(request, user.UUID);

                return Ok(result);
            }
            catch (NotSupportedException)
            {
                return BadRequest(new GatewayError(Errors.CodeLanguageNotSupported));
            }
            catch (ClientUnexpectedErrorException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogError("[Create]: Code problem not found.");

                return NotFound(Errors.CodeProblemNotFound.Message);
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError("[Create]: {entityName} with id {entityUUID} was not found", ex.EntityName, ex.EntityUUID);

                return NotFound(new GatewayError(Errors.CodeProblemNotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Create]: Unexpected error occurred while submiting code");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }

        [Authorize]
        [HttpGet("{runUUID}/progress")]
        public async Task<ActionResult<CodeRunProgress>> GetProgress([FromRoute] Guid runUUID)
        {
            var user = _authorizationService.GetUser(HttpContext.User);
            try
            {
                var result = await _codeSubmitService.GetProgress(user.UUID, runUUID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetProgress]: Unexpected error occurred while getting progress for {runUUID}", runUUID);

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }
    }
}
