using PhtgrphrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Repositories
{
    public interface IGalleryRepository
    {
        int CreateGallery(Gallery gallery);

        bool CreateGalleryAccessToken(GalleryAccessToken galleryAccessToken);

        List<Gallery> GetGalleries();

        Gallery GetGalleryByGuid(Guid guid);

        GalleryAccessToken GetGalleryAccessTokenByToken(Guid token);

        //public List<Image> GetImagesByPage(Gallery gallery, int pageSize, int pageNumber);

        //public int GetTotalImageCount(Gallery gallery);

        bool UpdateGallery(Gallery gallery);

        bool DeleteGallery(Gallery gallery);

        Image GetImageById(int id);

        bool CreateImage(Image image);

        bool UpdateImage(Image image);

        bool DeleteImage(Image image);

        List<Image> GetImagesByGalleryId(int iD);

        int GetGalleryAccessTokenCount();

        int GetImageCount();
    }
}
