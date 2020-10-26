using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PhtgrphrAPI.DbContexts;
using PhtgrphrAPI.Logic;
using PhtgrphrAPI.Models;
using PhtgrphrAPI.Repositories;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Controllers
{
    [ApiController]
    [Route("v1/admin/user")]
    public class UserController : PhtgrphrController
    {
        [HttpPost]
        [Route("authenticate/{username}")]
        public ActionResult<PhtgrphrResponse<TokenResponse>> Authenticate(string username, [FromBody] string password, [FromServices] IUserLogic userLogic)
        {
            return AsActionResult<TokenResponse>(userLogic.Authenticate(username, password));
        }
    }
}
