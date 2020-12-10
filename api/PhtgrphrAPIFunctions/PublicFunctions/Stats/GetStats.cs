using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhtgrphrAPI.Logic;
using PhtgrphrAPI.Responses;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;



namespace PhtgrphrAPIFunctions.Public.Stats
{
    public class GetStats : BaseFunction
    {
        private IStatsLogic _statsLogic;

        public GetStats()
        {
            _statsLogic = GetStatsLogic();
        }

        [FunctionName("PublicStatsGetStats")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/public/stats")] HttpRequest req)
        {
            return AsActionResult(_statsLogic.GetStats());
        }
    }
}
