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

namespace PhtgrphrAPIFunctions.Admin.Gallery
{
    public class DeleteGallery : BaseFunction
    {
        private IGalleryLogic _galleryLogic;
        private IFileManager _fileManager;

        //public DeleteGallery(IGalleryLogic galleryLogic)
        public DeleteGallery()
        {
            _galleryLogic = GetGalleryLogic();
            _fileManager = GetFileManager();
        }

        [FunctionName("AdminGalleryDeleteGallery")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/admin/gallery/delete/{token}/{galleryId}")] HttpRequest req,
            Guid token,
            int galleryId)
        {
            return AsActionResult(_galleryLogic.DeleteGalleryByGalleryId(token, galleryId, _fileManager));
        }
    }
}
