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
    public class DeleteImage : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        //public DeleteImage(IGalleryLogic galleryLogic)
        public DeleteImage()
        {
            _galleryLogic = GetGalleryLogic();
        }

        [FunctionName("AdminGalleryDeleteImage")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/admin/gallery/image/delete/{token}/{imageId}")] HttpRequest req,
            Guid token,
            int imageId)
        {
            return AsActionResult(_galleryLogic.DeleteImageByImageId(token, imageId));
        }
    }
}
