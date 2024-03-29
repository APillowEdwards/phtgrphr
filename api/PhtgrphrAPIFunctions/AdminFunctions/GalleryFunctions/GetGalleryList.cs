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
    public class GetGalleryList : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        //public GetGalleryList(IGalleryLogic galleryLogic)
        public GetGalleryList()
        {
            _galleryLogic = GetGalleryLogic();
        }

        [FunctionName("AdminGalleryGetGalleryList")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/admin/gallery/{token}")] HttpRequest req,
            Guid token)
        {
            return AsActionResult(_galleryLogic.GetGalleriesByUserAccessToken(token));
        }
    }
}
