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
    public class Exists : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        //public Exists(IGalleryLogic galleryLogic)
        public Exists()
        {
            _galleryLogic = GetGalleryLogic();
        }

        [FunctionName("PublicGalleryExists")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/public/gallery/exists/{guid}")] HttpRequest req,
            Guid guid)
        {
            return AsActionResult(_galleryLogic.Exists(guid));
        }
    }
}
