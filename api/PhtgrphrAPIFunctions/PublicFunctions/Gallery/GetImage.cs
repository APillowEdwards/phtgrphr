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
    public class GetImage : BaseFunction
    {
        private IGalleryLogic _galleryLogic;
        private IFileManager _fileManager;

        //public GetImage(IGalleryLogic galleryLogic, IFileManager fileManager)
        public GetImage()
        {
            _galleryLogic = GetGalleryLogic();
            _fileManager = GetFileManager();
        }

        [FunctionName("PublicGalleryGetImage")]
        public ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/public/gallery/image/{token}/{imageId}")] HttpRequest req,
            Guid token,
            int imageId)
        {
            ResponseFile file = _galleryLogic.GetImageFileWithGalleryAccessToken(token, imageId, _fileManager);

            return new FileStreamResult(file.File, file.MimeType);
        }
    }
}
