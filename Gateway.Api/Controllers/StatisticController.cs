using CodeRunManager.Contract.Interfaces;
using CodeRunManager.Contract.Models;
using Gateway.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Extensions;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticController : ControllerBase
    {
        private readonly ICodeRunManagerClient _codeRunManagerClient;
        private readonly IGatewayAuthorizationService _authorizationService;

        public StatisticController(ICodeRunManagerClient codeRunManagerClient, IGatewayAuthorizationService authorizationService)
        {
            _codeRunManagerClient = codeRunManagerClient.ThrowIfNull();
            _authorizationService = authorizationService.ThrowIfNull();
        }

        [HttpGet]
        public async Task<ActionResult<UserStatistic>> Get()
        {
            var user = _authorizationService.GetUser(User);

            var result = await _codeRunManagerClient.GetUserStatisticAsync(user.UUID);

            return Ok(result);
        }
    }
}
