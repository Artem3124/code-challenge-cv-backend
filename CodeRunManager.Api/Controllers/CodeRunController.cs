using CodeRunManager.Api.Interfaces;
using CodeRunManager.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Extensions;

namespace CodeRunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeRunController : ControllerBase
    {
        private readonly IQueueService _queueService;
        private readonly ILogger<CodeRunController> _logger;

        public CodeRunController(IQueueService queueService, ILogger<CodeRunController> logger)
        {
            _queueService = queueService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CodeRunQueueRequest request)
        {
            try
            {
                var result = await _queueService.EnqueueAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception occurred while submiting code run");

                return BadRequest("Unexpected error");
            }
        }
    }
}
