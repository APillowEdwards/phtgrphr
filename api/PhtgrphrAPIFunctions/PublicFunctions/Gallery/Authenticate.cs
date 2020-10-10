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



namespace PhtgrphrAPIFunctions.Public.Gallery
{
    public class Authenticate : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        //public Authenticate(IGalleryLogic galleryLogic)
        public Authenticate()
        {
            _galleryLogic = GetGalleryLogic();
        }

        [FunctionName("PublicGalleryAuthenticate")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/public/gallery/authenticate/{guid}")] HttpRequest req,
            Guid guid)
        {
            string password = new StreamReader(req.Body).ReadToEnd();

            password = password.Substring(1, password.Length - 2); // Since the body is a JSON string, snip the enclosing quotes

            return AsActionResult(_galleryLogic.Authenticate(guid, password));
        }
    }
}
