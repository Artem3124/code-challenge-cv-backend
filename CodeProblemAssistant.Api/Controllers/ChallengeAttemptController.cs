using CodeProblemAssistant.Api.Services;
using CodeProblemAssistant.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Extensions;

namespace CodeProblemAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengeAttemptController : ControllerBase
    {
        private readonly IChallengeAttemptService _challengeAttemptService;

        public ChallengeAttemptController(IChallengeAttemptService challengeAttemptService)
        {
            _challengeAttemptService = challengeAttemptService.ThrowIfNull();
        }

        [HttpPost]
        public async Task<ActionResult<ChallengeAttempt>> Create(ChallengeStartRequest request)
        {
            var result = await _challengeAttemptService.StartAsync(request);

            return Ok(result);
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<ChallengeAttempt>> Read(Guid uuid)
        {
            var result = await _challengeAttemptService.GetAttemptAsync(uuid);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update(ChallengeSubmitRequest request)
        {
            await _challengeAttemptService.SubmitAsync(request);

            return Ok(true);
        }


        [HttpPut("patch/{uuid}")]
        public async Task<ActionResult<bool>> Patch(Guid uuid, ChallengeUpdateRequest request)
        {
            var result = await _challengeAttemptService.Patch(uuid, request);

            return Ok(result);
        }
    }
}
