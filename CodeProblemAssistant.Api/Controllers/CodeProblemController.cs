using CodeProblemAssistant.Api.Interfaces;
using CodeProblemAssistant.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace CodeProblemAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeProblemController : ControllerBase
    {
        private readonly ICodeProblemService _codeProblemService;
        private readonly ILogger<CodeProblemController> _logger;
        private readonly ICodeTemplateService _codeTemplateService;

        public CodeProblemController(ICodeProblemService codeProblemService, ILogger<CodeProblemController> logger, ICodeTemplateService codeTemplateService)
        {
            _codeProblemService = codeProblemService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
            _codeTemplateService = codeTemplateService.ThrowIfNull();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CodeProblemCreateRequest request)
        {
            try
            {
                var result = await _codeProblemService.CreateAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexected error ocurred while creating code problem");

                return BadRequest("Unexpected error");
            }
        }

        [HttpGet("{codeProblemUUID}")]
        public async Task<IActionResult> Read([FromRoute] Guid codeProblemUUID)
        {
            try
            {
                var result = await _codeProblemService.GetAsync(codeProblemUUID);

                return Ok(result);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound($"Code problem {codeProblemUUID} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexected error ocurred while getting code problem {codeProblemUUID}.", codeProblemUUID);

                return BadRequest("Unexpected error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var result = await _codeProblemService.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexected error ocurred while getting code problems.");

                return BadRequest("Unexpected error.");
            }
        }

        [HttpGet("{codeProblemUUID}/runTestSet")]
        public async Task<IActionResult> GetRunTestCaseSet([FromRoute] Guid codeProblemUUID)
        {
            try
            {
                var result = await _codeProblemService.GetRunTestCaseSetAsync(codeProblemUUID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexected error ocurred while getting test cases for {codeProblemUUID}", codeProblemUUID);

                return BadRequest("Unexpected error.");
            }

        }

        [HttpGet("{codeProblemUUID}/submitTestSet")]
        public async Task<IActionResult> GetSubmitTestCaseSet([FromRoute] Guid codeProblemUUID)
        {
            try
            {
                var result = await _codeProblemService.GetSubmitTestCaseSetAsync(codeProblemUUID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexected error ocurred while getting test cases for {codeProblemUUID}", codeProblemUUID);

                return BadRequest("Unexpected error.");
            }
        }

        [HttpGet("{codeProblemUUID}/template/{codeLanguage}")]
        public async Task<ActionResult> GetTemplate([FromRoute] Guid codeProblemUUID, [FromRoute] CodeLanguage codeLanguage)
        {
            try
            {
                var result = await _codeTemplateService.GetSolutionTemplate(codeProblemUUID, codeLanguage);

                return Ok(result);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound($"Code problem {codeProblemUUID} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexected error ocurred while getting template for {codeProblemUUID}", codeProblemUUID);

                return BadRequest("Unexpected error.");
            }
        }

        [HttpGet("{challengeUUID}/challengeTemplate/{codeLanguage}")]
        public async Task<ActionResult> GetChallengeTemplate([FromRoute] Guid challengeUUID, [FromRoute] CodeLanguage codeLanguage)
        {
            try
            {
                var result = await _codeTemplateService.GetChallegenTemplate(challengeUUID, codeLanguage);

                return Ok(result);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound($"Code problem {challengeUUID} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexected error ocurred while getting template for {codeProblemUUID}", challengeUUID);

                return BadRequest("Unexpected error.");
            }
        }
    }
}
