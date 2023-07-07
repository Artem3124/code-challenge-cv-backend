using CodeProblemAssistant.Api.Services;
using CodeProblemAssistant.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Extensions;

namespace CodeProblemAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeAttemptService _challengeAttemptService;

        public ChallengeController(IChallengeAttemptService challengeAttemptService)
        {
            _challengeAttemptService = challengeAttemptService.ThrowIfNull();
        }

        [HttpGet("{userUUID}/{challengeUUID}")]
        public async Task<ActionResult<Challenge>> Read(Guid userUUID, Guid challengeUUID)
        {
            var challenge = await _challengeAttemptService.GetAsync(challengeUUID, userUUID);

            return Ok(challenge);
        }

        [HttpGet("{userUUID}")]
        public async Task<ActionResult<List<Challenge>>> Read(Guid userUUID)
        {
            var result = await _challengeAttemptService.GetAsync(userUUID);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(ChallengeCreateRequest request)
        {
            var result = await _challengeAttemptService.CreateAsync(request);

            return Ok(result);
        }
    }
}
