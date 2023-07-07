using CodeProblemAssistant.Api.Interfaces;
using CodeProblemAssistant.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Extensions;

namespace CodeProblemAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeProblemTagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public CodeProblemTagController(ITagService tagService)
        {
            _tagService = tagService.ThrowIfNull();
        }

        [HttpPut]
        public async Task<ActionResult<List<Tag>>> Query(TagQueryRequest request)
        {
            var result = await _tagService.Query(request);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(TagCreateRequest request)
        {
            var result = await _tagService.GetOrCreateAsync(request.Name, request.CodeProblemId);

            return Ok(result.Id);
        }

        [HttpPost("batch")]
        public async Task<ActionResult<int>> PostBatch(TagCreateRequestBatch request)
        {
            var result = await _tagService.GetOrCreateBatchAsync(request.Names, request.CodeProblemUUID);

            return Ok(result.Count);
        }
    }
}
