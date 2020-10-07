using Microsoft.AspNetCore.Mvc;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhtgrphrAPIFunctions
{
    public class BaseFunction
    {
        public ActionResult AsActionResult<T>(PhtgrphrResponse<T> response)
        {
            switch (response.Code)
            {
                case 200:
                    return new OkObjectResult(response);
                case 401:
                    return new UnauthorizedObjectResult(response);
                case 404:
                    return new NotFoundObjectResult(response);
                default:
                    throw new Exception("Invalid response code.");
            }
        }
    }
}
