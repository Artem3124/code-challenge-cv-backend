using CodeRunManager.Api.Interfaces;
using CodeRunManager.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Core.Extensions;

namespace CodeRunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeRunStageController : ControllerBase
    {
        private readonly IStagePatchService _stagePatchService;

        private readonly ICodeRunStageService _codeRunStageService;

        public CodeRunStageController(IStagePatchService stagePatchService, ICodeRunStageService codeRunStageService)
        {
            _stagePatchService = stagePatchService.ThrowIfNull();
            _codeRunStageService = codeRunStageService.ThrowIfNull();
        }

        [HttpPut("{codeRunUUID}")]
        public async Task<ActionResult<int>> Update(Guid codeRunUUID, CodeRunStageUpdateRequest request)
        {
            var result = await _stagePatchService.PatchAsync(codeRunUUID, request.Stage);

            return Ok(result);
        }

        [HttpPost("{codeRunUUID}")]
        public async Task<ActionResult<int>> Complete(Guid codeRunUUID, CodeRunCompleteRequest request)
        {
            await _stagePatchService.PatchAsync(codeRunUUID, CodeRunStage.Completed);

            var result = await _codeRunStageService.CompleteAsync(codeRunUUID, request);

            return Ok(result);
        }
    }
}
