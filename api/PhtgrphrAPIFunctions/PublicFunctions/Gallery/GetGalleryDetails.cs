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
    public class GetGalleryDetails : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        //public GetGalleryDetails(IGalleryLogic galleryLogic)
        public GetGalleryDetails()
        {
            _galleryLogic = GetGalleryLogic();
        }

        [FunctionName("PublicGalleryGetGalleryDetails")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/public/gallery/{token}")] HttpRequest req,
            Guid token)
        {
            return AsActionResult(_galleryLogic.GetGalleryDetailsByGalleryAccessToken(token));
        }
    }
}
