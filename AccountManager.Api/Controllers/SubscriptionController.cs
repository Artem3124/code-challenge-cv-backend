using AccountManager.Api.Interfaces;
using AccountManager.Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace AccountManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(ISubscriptionService subscriptionService, ILogger<SubscriptionController> logger)
        {
            _subscriptionService = subscriptionService.ThrowIfNull();
            _logger = logger.ThrowIfNull();
        }

        [HttpGet]
        [Route("{userUUID}")]
        public async Task<ActionResult<SubscriptionType>> Get([FromRoute] Guid userUUID)
        {
            try
            {
                var result = await _subscriptionService.GetByUserUUIDAsync(userUUID);

                return Ok(result);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]: Unexpected error.");

                return BadRequest();
            }
        }
    }
}
