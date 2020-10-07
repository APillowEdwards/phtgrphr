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

[assembly: FunctionsStartup(typeof(PhtgrphrAPIFunctions.Startup))]

namespace PhtgrphrAPIFunctions.Admin.User
{
    public class Authenticate : BaseFunction
    {
        private IUserLogic _userLogic;

        public Authenticate(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [FunctionName("AdminUserAuthenticate")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/admin/user/authenticate/{username}")] HttpRequest req,
            string username)
        {
            string password = new StreamReader(req.Body).ReadToEnd();

            password = password.Substring(1, password.Length - 2); // Since the body is a JSON string, snip the enclosing quotes

            return AsActionResult(_userLogic.Authenticate(username, password));
        }
    }
}
