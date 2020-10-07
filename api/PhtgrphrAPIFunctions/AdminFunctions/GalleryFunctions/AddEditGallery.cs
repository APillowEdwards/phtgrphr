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

[assembly: FunctionsStartup(typeof(PhtgrphrAPIFunctions.Startup))]

namespace PhtgrphrAPIFunctions.Admin.Gallery
{
    public class AddEditGallery : BaseFunction
    {
        private IGalleryLogic _galleryLogic;

        public AddEditGallery(IGalleryLogic galleryLogic)
        {
            _galleryLogic = galleryLogic;
        }

        [FunctionName("AdminGalleryAddEditGallery")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/admin/gallery/{token}")] HttpRequest req,
            Guid token,
            int imageId)
        {
            string galleryString = new StreamReader(req.Body).ReadToEnd();

            PhtgrphrAPI.Models.Gallery gallery = (PhtgrphrAPI.Models.Gallery) JsonConvert.DeserializeObject(galleryString);

            return AsActionResult(_galleryLogic.AddEditGallery(token, gallery));
        }
    }
}
