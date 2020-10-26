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
using System.Collections.Generic;
using System.Linq;
using PhtgrphrAPI.FileManagers;



namespace PhtgrphrAPIFunctions.Admin.Gallery
{
    public class UploadImages : BaseFunction
    {
        private IGalleryLogic _galleryLogic;
        private IFileManager _fileManager;

        //public UploadImages(IGalleryLogic galleryLogic, IFileManager fileManager)
        public UploadImages()
        {
            _galleryLogic = GetGalleryLogic();
            _fileManager = GetFileManager();
        }

        [FunctionName("AdminGalleryUploadImages")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/admin/gallery/images/{token}/{galleryId}")] HttpRequest req,
            Guid token,
            int galleryId)
        {
            List<IFormFile> files = req.Form.Files.ToList();

            return AsActionResult(_galleryLogic.AddImagesToGallery(token, galleryId, files, _fileManager));
        }
    }
}
