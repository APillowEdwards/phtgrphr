using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhtgrphrAPI.DbContexts;
using PhtgrphrAPI.Logic;
using PhtgrphrAPI.Models;
using PhtgrphrAPI.Repositories;
using PhtgrphrAPI.Responses;
using System.IO;
using PhtgrphrAPI.FileManagers;

namespace PhtgrphrAPI.Controllers.Admin
{
    [ApiController]
    [Route("admin/gallery")]
    public class GalleryController : PhtgrphrController
    {
        private readonly IConfiguration _configuration;

        public GalleryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{token}")]
        public ActionResult<PhtgrphrResponse<GalleryListResponse>> Get(Guid token, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<GalleryListResponse>(galleryLogic.GetGalleriesByUserAccessToken(token));
        }

        [HttpGet]
        [Route("{token}/{galleryId}")]
        public ActionResult<PhtgrphrResponse<GalleryResponse>> GetGalleryDetails(Guid token, int galleryId, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<GalleryResponse>(galleryLogic.GetGalleryDetailsByGalleryId(token, galleryId));
        }

        [HttpGet]
        [Route("images/{token}/{galleryId}")]
        public ActionResult<PhtgrphrResponse<ImageListResponse>> GetImages(Guid token, int galleryId, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<ImageListResponse>(galleryLogic.GetGalleryImagesByGalleryId(token, galleryId));
        }

        [HttpGet]
        [Route("image/{token}/{imageId}")]
        public ActionResult GetImage(Guid token, int imageId, [FromServices] IFileManager fileManager, [FromServices] IGalleryLogic galleryLogic)
        {
            FileManagerFile file = galleryLogic.GetImageFileWithUserAccessToken(token, imageId, fileManager);

            return File(file.File, file.MimeType);
        }

        [HttpPost]
        [Route("{token}")]
        public ActionResult<PhtgrphrResponse<GalleryResponse>> AddEdit(Guid token, [FromBody] Gallery gallery, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<GalleryResponse>(galleryLogic.AddEditGallery(token, gallery));
        }

        [HttpPost]
        [Route("delete/{token}/{galleryId}")]
        public ActionResult<PhtgrphrResponse<Dictionary<string, bool>>> DeleteGallery(Guid token, int galleryId, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<Dictionary<string, bool>>(galleryLogic.DeleteGalleryByGalleryId(token, galleryId));
        }

        [HttpPost]
        [Route("images/{token}/{galleryId}")]
        public ActionResult<PhtgrphrResponse<Dictionary<string, bool>>> UploadImages(Guid token, int galleryId, [FromForm] List<IFormFile> images, [FromServices] IFileManager fileManager, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<Dictionary<string, bool>>(galleryLogic.AddImagesToGallery(token, galleryId, images, fileManager));
        }

        [HttpPost]
        [Route("images/sort/{token}/{galleryId}")]
        public ActionResult<PhtgrphrResponse<Dictionary<string, bool>>> SortImages(Guid token, int galleryId, [FromBody] List<Image> images, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<Dictionary<string, bool>>(galleryLogic.SortImages(token, galleryId, images));
        }

        [HttpPost]
        [Route("image/delete/{token}/{imageId}")]
        public ActionResult<PhtgrphrResponse<Dictionary<string, bool>>> DeleteImage(Guid token, int imageid, [FromServices] IGalleryLogic galleryLogic)
        {
            return AsActionResult<Dictionary<string, bool>>(galleryLogic.DeleteImageByImageId(token, imageid));
        }
    }
}
