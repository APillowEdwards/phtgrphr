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

namespace PhtgrphrAPIFunctions.Admin.Gallery
{
    public class GetGalleryDetails : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        public GetGalleryDetails(IGalleryLogic galleryLogic)
        {
            _galleryLogic = galleryLogic;
        }

        [FunctionName("AdminGalleryGetGalleryDetails")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/admin/gallery/{token}/{galleryId}")] HttpRequest req,
            Guid token,
            int galleryId)
        {
            return AsActionResult(_galleryLogic.GetGalleryDetailsByGalleryId(token, galleryId));
        }
    }
}
