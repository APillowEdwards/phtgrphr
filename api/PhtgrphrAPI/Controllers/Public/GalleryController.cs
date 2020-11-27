using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhtgrphrAPI.FileManagers;
using PhtgrphrAPI.Logic;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.IO;

namespace PhtgrphrAPI.Controllers
{
    [ApiController]
    [Route("v1/public/gallery")]
    public class GalleryController : PhtgrphrController
    {
        private readonly IConfiguration _configuration;

        public GalleryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{token}")]
        public ActionResult<PhtgrphrResponse<GalleryResponse>> GetDetails(Guid token, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<GalleryResponse>(galleryLogic.GetGalleryDetailsByGalleryAccessToken(token));
        }

        [HttpGet]
        [Route("exists/{guid}")]
        public ActionResult<PhtgrphrResponse<Dictionary<string, bool>>> Exists(Guid guid, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<Dictionary<string, bool>>(galleryLogic.Exists(guid));
        }

        [HttpGet]
        [Route("images/{token}/{pageSize}/{pageNumber}")]
        public ActionResult<PhtgrphrResponse<ImageListResponse>> GetGalleryImagesByPage(Guid token, int pageSize, int pageNumber, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<ImageListResponse>(galleryLogic.GetGalleryImagesByPage(token, pageSize, pageNumber));
        }

        [HttpPost]
        [Route("authenticate/{guid}")]
        public ActionResult<PhtgrphrResponse<TokenResponse>> Authenticate(Guid guid, [FromBody] string password, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<TokenResponse>(galleryLogic.Authenticate(guid, password));
        }

        [HttpGet]
        [Route("image/{token}/{imageId}")]
        public ActionResult GetImage(Guid token, int imageId, [FromServices] IFileManager fileManager, [FromServices] IGalleryLogic galleryLogic)
        {
            ResponseFile file = galleryLogic.GetImageFileWithGalleryAccessToken(token, imageId, fileManager);

            Request.HttpContext.Response.Headers.Add("Content-Disposition", "attachment");

            return File(file.File, file.MimeType);
        }

        [HttpGet]
        [Route("images/download/{token}")]
        public ActionResult GetGalleryImagesAsZip(Guid token, [FromServices] IFileManager fileManager, [FromServices] IGalleryLogic galleryLogic)
        {
            ResponseFile file = galleryLogic.GetGalleryImagesAsZipByGalleryAccessToken(token, fileManager);

            return File(file.File, file.MimeType, file.Name);
        }
    }
}
