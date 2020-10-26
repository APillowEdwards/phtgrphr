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
using PhtgrphrAPI.FileManagers;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;



namespace PhtgrphrAPIFunctions.Admin.Gallery
{
    public class SortImages : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        //public SortImages(IGalleryLogic galleryLogic)
        public SortImages()
        {
            _galleryLogic = GetGalleryLogic();
        }

        [FunctionName("AdminGallerySortImages")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/admin/gallery/images/sort/{token}/{galleryId}")] HttpRequest req,
            Guid token,
            int galleryId)
        {
            string imagesString = new StreamReader(req.Body).ReadToEnd();

            var images = ((JArray) JsonConvert.DeserializeObject(imagesString)).ToObject<List<PhtgrphrAPI.Models.Image>>();

            return AsActionResult(_galleryLogic.SortImages(token, galleryId, images));
        }
    }
}
