using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Org.BouncyCastle.Bcpg;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Controllers
{
    public class PhtgrphrController : ControllerBase
    {
        public ActionResult<PhtgrphrResponse<T>> AsActionResult<T>(PhtgrphrResponse<T> response)
        {
            switch (response.Code)
            {
                case 200:
                    return Ok(response);
                case 401:
                    return Unauthorized(response);
                case 404:
                    return NotFound(response);
                default:
                    throw new Exception("Invalid response code.");
            }
        }
    }
}
