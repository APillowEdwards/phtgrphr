using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhtgrphrAPI.FileManagers;
using PhtgrphrAPI.Models;
using PhtgrphrAPI.Repositories;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Logic
{
    public interface IGalleryLogic
    {
        PhtgrphrResponse<GalleryListResponse> GetGalleriesByUserAccessToken(Guid token);

        PhtgrphrResponse<GalleryResponse> GetGalleryDetailsByGalleryAccessToken(Guid token);

        PhtgrphrResponse<Dictionary<string, bool>> Exists(Guid guid);

        PhtgrphrResponse<ImageListResponse> GetGalleryImagesByPage(Guid token, int pageSize, int pageNumber);

        PhtgrphrResponse<TokenResponse> Authenticate(Guid guid, string password);

        Gallery SanitiseGallery(Gallery gallery);

        GalleryAccessToken GetGalleryAccessTokenByToken(Guid token);

        PhtgrphrResponse<ImageListResponse> GetGalleryImagesByGalleryId(Guid token, int galleryId);

        PhtgrphrResponse<GalleryResponse> GetGalleryDetailsByGalleryId(Guid token, int galleryId);

        PhtgrphrResponse<GalleryResponse> AddEditGallery(Guid token, Gallery gallery);

        PhtgrphrResponse<Dictionary<string, bool>> DeleteGalleryByGalleryId(Guid token, int galleryId);

        PhtgrphrResponse<Dictionary<string, bool>> AddImagesToGallery(Guid token, int galleryId, List<IFormFile> files, IFileManager fileManager);

        FileManagerFile GetImageFileWithGalleryAccessToken(Guid token, int imageId, IFileManager fileManager);

        FileManagerFile GetImageFileWithUserAccessToken(Guid token, int imageId, IFileManager fileManager);

        PhtgrphrResponse<Dictionary<string, bool>> SortImages(Guid token, int galleryId, List<Image> images);

        PhtgrphrResponse<Dictionary<string, bool>> DeleteImageByImageId(Guid token, int imageid);
    }
}
