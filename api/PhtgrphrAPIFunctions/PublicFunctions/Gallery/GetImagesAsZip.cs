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

namespace PhtgrphrAPIFunctions.Public.Gallery
{
    public class GetImagesAsZip : BaseFunction
    {
        private IGalleryLogic _galleryLogic;
        private IFileManager _fileManager;

        public GetImagesAsZip()
        {
            _galleryLogic = GetGalleryLogic();
            _fileManager = GetFileManager();
        }

        [FunctionName("PublicGalleryGetImagesAsZip")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/public/gallery/images/download/{token}")] HttpRequest req,
            Guid token)
        {
            ResponseFile file = _galleryLogic.GetGalleryImagesAsZipByGalleryAccessToken(token, _fileManager);

            var result = new FileStreamResult(file.File, file.MimeType);
            result.FileDownloadName = file.Name;

            return result;
        }
    }
}
