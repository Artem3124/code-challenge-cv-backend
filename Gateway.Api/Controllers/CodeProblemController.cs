using Gateway.Api.Interfaces;
using Gateway.Api.Services;
using Gateway.Contact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Constants;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using System.Net;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeProblemController : ControllerBase
    {
        private readonly ICodeProblemProvider _codeProblemProvider;
        private readonly ILogger<CodeProblemController> _logger;
        private readonly IGatewayAuthorizationService _authorizationService;

        public CodeProblemController(ICodeProblemProvider codeProblemProvider, ILogger<CodeProblemController> logger, IGatewayAuthorizationService authorizationService)
        {
            _codeProblemProvider = codeProblemProvider.ThrowIfNull();
            _logger = logger.ThrowIfNull();
            _authorizationService = authorizationService.ThrowIfNull();
        }

        [HttpGet("{codeProblemUUID}")]
        public async Task<ActionResult<CodeProblem>> Read([FromRoute] Guid codeProblemUUID)
        {
            try
            {
                var result = await _codeProblemProvider.Get(codeProblemUUID);

                return result;
            }
            catch (ClientUnexpectedErrorException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new GatewayError(Errors.CodeProblemNotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Read]: While getting code problem {codeProblemUUID} unexpected error occurred", codeProblemUUID);

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CodeProblem>>> ReadAll()
        {
            try
            {
                var user = _authorizationService.GetUser(User);

                var result = await _codeProblemProvider.GetForUser(user.UUID);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ReadAll]: Unexpected error.");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateCodeProblemRequest request)
        {
            try
            {
                // TODO: Add role checker
                var result = await _codeProblemProvider.CreateAsync(request);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Create]: Unexpected error.");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }
    }
}
