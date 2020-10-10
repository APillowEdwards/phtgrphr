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
    public class GetGalleryImagesByPage : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        //public GetGalleryImagesByPage(IGalleryLogic galleryLogic)
        public GetGalleryImagesByPage()
        {
            _galleryLogic = GetGalleryLogic();
        }

        [FunctionName("PublicGalleryGetGalleryImagesByPage")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/public/gallery/images/{token}/{pageSize}/{pageNumber}")] HttpRequest req,
            Guid token,
            int pageSize,
            int pageNumber)
        {
            return AsActionResult(_galleryLogic.GetGalleryImagesByPage(token, pageSize, pageNumber));
        }
    }
}
