using CodeRunManager.Api.Interfaces;
using CodeRunManager.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Extensions;

namespace CodeRunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService.ThrowIfNull();
        }

        [HttpGet("{userUUID}")]
        public async Task<ActionResult<UserStatistic>> Get(Guid userUUID)
        {
            var result = await _statisticService.GetStatisticForUser(userUUID);

            return Ok(result);
        }
    }
}
