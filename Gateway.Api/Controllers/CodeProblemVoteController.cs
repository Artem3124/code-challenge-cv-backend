using CodeProblemAssistant.Contract.Clients;
using CodeProblemAssistant.Contract.Models;
using Gateway.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Extensions;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeProblemVoteController : ControllerBase
    {
        private readonly ICodeProblemAssistantClient _codeProblemAssistantClient;
        private readonly IGatewayAuthorizationService _authorizationService;

        public CodeProblemVoteController(ICodeProblemAssistantClient codeProblemAssistantClient, IGatewayAuthorizationService authorizationService)
        {
            _codeProblemAssistantClient = codeProblemAssistantClient.ThrowIfNull();
            _authorizationService = authorizationService.ThrowIfNull();
        }

        public class CodeProblemVoteRequest
        {
            public Guid CodeProblemUUID { get; set; }

            public bool VoteUp { get; set; }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Vote(CodeProblemVoteRequest request)
        {
            var user = _authorizationService.GetUser(User);

            await _codeProblemAssistantClient.CodeProblemVotePatch(new CodeProblemVotePatchRequest
            {
                CodeProblemUUID = request.CodeProblemUUID,
                UserReferenceUUID = user.UUID,
                UpVote = request.VoteUp,
            });

            return NoContent();
        }

        [Authorize]
        [HttpGet("{codeProblemUUID}")]
        public async Task<ActionResult<List<Vote>>> Get(Guid codeProblemUUID)
        {
            var user = _authorizationService.GetUser(User);

            var result = await _codeProblemAssistantClient.QueryCodeProblemVotes(new CodeProblemVotesQueryRequest
            {
                CodeProblemUUID = codeProblemUUID,
                UserReferenceUUID = user.UUID,
            });

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Vote>>> Get()
        {
            var user = _authorizationService.GetUser(User);

            var result = await _codeProblemAssistantClient.QueryCodeProblemVotes(new CodeProblemVotesQueryRequest
            {
                UserReferenceUUID = user.UUID,
            });

            return Ok(result);
        }
    }
}
