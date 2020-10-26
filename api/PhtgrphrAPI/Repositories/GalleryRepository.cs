using Microsoft.EntityFrameworkCore;
using PhtgrphrAPI.DbContexts;
using PhtgrphrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        private PhtgrphrContext context;

        public GalleryRepository(PhtgrphrContext context)
        {
            this.context = context;
        }

        public int CreateGallery(Gallery gallery)
        {
            context.Galleries.Add(gallery);

            if (context.SaveChanges() < 1)
            {
                return -1;
            }

            return gallery.ID;
        }

        public bool CreateGalleryAccessToken(GalleryAccessToken galleryAccessToken)
        {
            context.GalleryAccessTokens.Add(galleryAccessToken);

            int result = context.SaveChanges();

            return result > 0;
        }

        public List<Gallery> GetGalleries()
        {
            return context.Galleries.ToList();
        }

        public Gallery GetGalleryByGuid(Guid guid)
        {
            return context.Galleries
                .Where(g => g.Guid == guid)
                .SingleOrDefault();
        }

        public GalleryAccessToken GetGalleryAccessTokenByToken(Guid token)
        {
            return context.GalleryAccessTokens
                .Where(gat => gat.Token == token)
                .Where(gat => gat.Expiry > DateTime.UtcNow)
                .Include("Gallery.Images")
                .SingleOrDefault();
        }

        public List<Image> GetImagesByPage(Gallery gallery, int pageSize, int pageNumber)
        {
            int skip = (pageNumber - 1) * pageSize;

            return gallery.Images
                .OrderBy(i => i.Sort)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalImageCount(Gallery gallery)
        {
            return gallery.Images
                .Count();
        }

        public bool UpdateGallery(Gallery gallery)
        {
            Gallery dbGallery = context.Galleries
                .Where(g => g.ID == gallery.ID)
                .SingleOrDefault();

            if (dbGallery == null)
            {
                return false;
            }

            dbGallery.Name = gallery.Name ?? dbGallery.Name;
            dbGallery.Password = gallery.Password ?? dbGallery.Password;

            int result = context.SaveChanges();

            return result > 0;
        }

        public bool DeleteGallery(Gallery gallery)
        {
            context.Galleries.Remove(gallery);

            int result = context.SaveChanges();

            return result > 0;
        }

        public Image GetImageById(int id)
        {
            return context.Images
                .Where(i => i.ID == id)
                .Include("Gallery.User")
                .SingleOrDefault();
        }

        public bool CreateImage(Image image)
        {
            context.Images.Add(image);

            int result = context.SaveChanges();

            return result > 0;
        }

        public bool UpdateImage(Image image)
        {
            Image dbImage = context.Images
                .Where(i => i.ID == image.ID)
                .SingleOrDefault();

            if (dbImage == null)
            {
                return false;
            }

            dbImage.Sort = image.Sort;

            int result = context.SaveChanges();

            return result > 0;
        }

        public bool DeleteImage(Image image)
        {
            context.Images.Remove(image);

            int result = context.SaveChanges();

            return result > 0;
        }

        public List<Image> GetImagesByGalleryId(int id)
        {
            return context.Images
                .Where(i => i.Gallery.ID == id)
                .OrderBy(i => i.Sort)
                .ToList();
        }
    }
}
