using CodeRunManager.Api.Interfaces;
using CodeRunManager.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Constants;
using Shared.Core.Enums;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using System.Net;

namespace CodeRunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeRunResultController : ControllerBase
    {
        private readonly ICodeRunStageService _codeRunStageService;
        private readonly ILogger<CodeRunResultController> _logger;
        private readonly ICodeRunResultService _codeRunResultService;

        public CodeRunResultController(ICodeRunStageService codeRunStageService, ICodeRunResultService codeRunResultService, ILogger<CodeRunResultController> logger)
        {
            _codeRunStageService = codeRunStageService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
            _codeRunResultService = codeRunResultService.ThrowIfNull();
        }

        [HttpGet("{runUUID}/stage")]
        public async Task<ActionResult<CodeRunStage>> GetStage([FromRoute] Guid runUUID)
        {
            try
            {
                var result = await _codeRunStageService.GetAsync(runUUID);

                return Ok(result);
            }
            catch (ClientUnexpectedErrorException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(Errors.CodeRunNotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetStage]: Unexpected error occurred while getting stage of {runUUID}", runUUID);

                return BadRequest(Errors.UnexpectedError);
            }
        }

        [HttpGet("{runUUID}")]
        public async Task<ActionResult<CodeRunResult>> Get([FromRoute] Guid runUUID)
        {
            try
            {
                var result = await _codeRunStageService.GetResultAsync(runUUID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]: Unexpected error occurred while getting result of {runUUID}", runUUID);

                return BadRequest(Errors.UnexpectedError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<CodeRunResult>>> Query([FromBody] CodeRunResultQueryRequest request)
        {
            try
            {
                var result = await _codeRunResultService.QueryAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Query]: Unexpected error occurred while querying code run results.");

                return BadRequest(Errors.UnexpectedError);
            }
        }

        [HttpPost]
        [Route("expanded")]
        public async Task<ActionResult<List<CodeRunResultExpanded>>> QueryExpanded([FromBody] CodeRunResultQueryRequest request)
        {
            try
            {
                var result = await _codeRunResultService.QueryExpandedAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[QueryExpanded]: Unexpected error occurred while querying code run results.");

                return BadRequest(Errors.UnexpectedError);
            }
        }
    }
}
