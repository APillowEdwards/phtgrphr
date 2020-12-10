using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhtgrphrAPI.Logic;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Controllers.Public
{
    [ApiController]
    [Route("v1/public/stats")]
    public class StatsController : PhtgrphrController
    {
        private readonly IConfiguration _configuration;

        public StatsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<PhtgrphrResponse<StatsResponse>> GetDetails([FromServices] IStatsLogic statsLogic)
        {
            return AsActionResult<StatsResponse>(statsLogic.GetStats());
        }
    }
}