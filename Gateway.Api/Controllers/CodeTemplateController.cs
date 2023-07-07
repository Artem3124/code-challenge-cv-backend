using Gateway.Api.Interfaces;
using Gateway.Contact.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Constants;
using Shared.Core.Enums;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using System.Net;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeTemplateController : ControllerBase
    {
        private readonly ICodeTemplateService _codeTemplateService;
        private readonly ILogger<CodeTemplateController> _logger;

        public CodeTemplateController(ICodeTemplateService codeTemplateService, ILogger<CodeTemplateController> logger)
        {
            _codeTemplateService = codeTemplateService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
        }

        [HttpGet("{codeProblemUUID}/template/{codeLanguage}")]
        public async Task<ActionResult<CodeProblemTemplate>> Get([FromRoute] Guid codeProblemUUID, [FromRoute] CodeLanguage codeLanguage)
        {
            try
            {
                var result = await _codeTemplateService.GetSolutionTemplateAsync(codeProblemUUID, codeLanguage);

                return Ok(result);
            }
            catch (ClientUnexpectedErrorException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new GatewayError(Errors.CodeProblemNotFound));
            }
            catch (NotSupportedException)
            {
                return BadRequest(new GatewayError(Errors.CodeLanguageNotSupported));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]: Unexpected error occurred while getting code problem template.");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }

        [HttpGet("Challenge/{challengeUUID}")]
        public async Task<ActionResult<List<CodeProblemTemplate>>> GetChallenge(Guid challengeUUID)
        {
            try
            {
                var result = await _codeTemplateService.GetChallengeTemplatesAsync(challengeUUID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]: Unexpected error occurred while getting code problem templates.");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<CodeProblemTemplate>>> Get(Guid codeProblemUUID)
        {
            try
            {
                var result = await _codeTemplateService.GetSolutionTemplatesAsync(codeProblemUUID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]: Unexpected error occurred while getting code problem templates.");

                return BadRequest(new GatewayError(Errors.UnexpectedError));
            }
        }
    }
}
