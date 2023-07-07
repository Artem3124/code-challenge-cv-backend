using CodeProblemAssistant.Api.Interfaces;
using CodeProblemAssistant.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace CodeProblemAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeProblemVoteController : ControllerBase
    {
        private readonly ICodeProblemVoteService _codeProblemVoteService;
        private readonly ILogger<CodeProblemVoteController> _logger;

        public CodeProblemVoteController(ICodeProblemVoteService codeProblemVoteService, ILogger<CodeProblemVoteController> logger)
        {
            _codeProblemVoteService = codeProblemVoteService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Patch(CodeProblemVotePatchRequest request)
        {
            try
            {
                await _codeProblemVoteService.Vote(request);

                return Ok(true);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "Unexpected error occurred while voting for codeProblem{codeProblemUUID} user: {userUUID}",
                    request.CodeProblemUUID, request.UserReferenceUUID);

                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Vote>>> Query(CodeProblemVotesQueryRequest request)
        {
            try
            {
                var result =  await _codeProblemVoteService.Query(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unexpected error ocurred while getting votes for user: {userUUID} codeProblem: {codeProblemUUID} vote: {vote} id: {id}",
                    request.UserReferenceUUID, request.CodeProblemUUID, request.Id, request.UpVote);

                return BadRequest();
            }
        }
}
    }
