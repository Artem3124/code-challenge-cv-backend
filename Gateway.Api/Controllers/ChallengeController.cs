using CodeProblemAssistant.Contract.Clients;
using CodeProblemAssistant.Contract.Models;
using Gateway.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Core.Extensions;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChallengeController : ControllerBase
    {
        private readonly ICodeProblemAssistantClient _codeProblemAssistantClient;
        private readonly IGatewayAuthorizationService _authorizationService;

        public ChallengeController(ICodeProblemAssistantClient codeProblemAssistantClient, IGatewayAuthorizationService authorizationService)
        {
            _codeProblemAssistantClient = codeProblemAssistantClient.ThrowIfNull();
            _authorizationService = authorizationService.ThrowIfNull();
        }

        [Authorize]
        [HttpGet("{challengeUUID}")]
        public async Task<ActionResult<Challenge>> Read(Guid challengeUUID)
        {
            var result = await _codeProblemAssistantClient.GetChallengeAsync(challengeUUID, _authorizationService.GetUser(User).UUID);

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Challenge>> Read()
        {
            var result = await _codeProblemAssistantClient.GetAllChallengesAsync(_authorizationService.GetUser(User).UUID);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(ChallengeCreateRequest request)
        {
            request.HostUUID = _authorizationService.GetUser(User).UUID;
            var result = await _codeProblemAssistantClient.CreateChallengeAsync(request);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("start/{challengeUUID}")]
        public async Task<ActionResult<ChallengeAttempt>> StartAttempt(Guid challengeUUID)
        {
            var result = await _codeProblemAssistantClient.StartAttemptAsync(new ChallengeStartRequest
            {
                ChallengeUUID = challengeUUID,
                UserUUID = _authorizationService.GetUser(User).UUID
            });

            return result;
        }

        [HttpGet("attempt/{uuid}")]
        public async Task<ActionResult<ChallengeAttempt>> GetAttempt(Guid uuid)
        {
            var result = await _codeProblemAssistantClient.GetChallengeAttempt(uuid);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult> SubmitAttempt(ChallengeSubmit request)
        {
            await _codeProblemAssistantClient.SubmitAttemptAsync(new ChallengeSubmitRequest
            {
                SourceCode = request.SourceCode,
                ChallengeUUID = request.ChallengeUUID,
                UserUUID = _authorizationService.GetUser(User).UUID,
                CodeLanguage = request.CodeLanguage,
            });

            return NoContent();
        }

        [HttpPatch("review/{uuid}")]
        public async Task<ActionResult> Review(Guid uuid, ChallengeUpdateRequest request)
        {
            await _codeProblemAssistantClient.PatchChallengeAttempt(uuid, request);

            return NoContent();
        }

        public class ChallengeSubmit
        {
            public Guid ChallengeUUID { get; set; }

            public string SourceCode { get; set; }

            public CodeLanguage CodeLanguage { get; set; }
        }
    }
}
