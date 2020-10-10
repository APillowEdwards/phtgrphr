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



namespace PhtgrphrAPIFunctions.Admin.Gallery
{
    public class GetGalleryImagesByGallery : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        //public GetGalleryImagesByGallery(IGalleryLogic galleryLogic)
        public GetGalleryImagesByGallery()
        {
            _galleryLogic = GetGalleryLogic();
        }

        [FunctionName("AdminGalleryGetGalleryImagesByGallery")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/admin/gallery/images/{token}/{galleryId}")] HttpRequest req,
            Guid token,
            int galleryId)
        {
            return AsActionResult(_galleryLogic.GetGalleryImagesByGalleryId(token, galleryId));
        }
    }
}
