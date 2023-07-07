using CodeProblemAssistant.Contract.Clients;
using CodeProblemAssistant.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Extensions;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeProblemTagController : ControllerBase
    {
        private readonly ICodeProblemAssistantClient _codeProblemAssistantClient;

        public CodeProblemTagController(ICodeProblemAssistantClient codeProblemAssistantClient)
        {
            _codeProblemAssistantClient = codeProblemAssistantClient.ThrowIfNull();
        }

        [HttpGet]
        public async Task<ActionResult<List<Tag>>> Get()
        {
            var result = await _codeProblemAssistantClient.QueryCodeProblemTags(new CodeProblemAssistant.Contract.Models.TagQueryRequest());

            return Ok(result);
        }
    }
}
