using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Responses
{
    public class GalleryResponse
    {
        public GalleryResponseGallery Gallery { get; set; }

        public GalleryResponse(Models.Gallery gallery)
        {
            Gallery = new GalleryResponseGallery(gallery.ID, gallery.Name, gallery.Password, gallery.Guid, gallery.ShowDownloadAll);
        }
    }
    public class GalleryListResponse
    {
        public List<GalleryResponseGallery> Galleries { get; set; }

        public GalleryListResponse(List<Models.Gallery> galleries)
        {
            Galleries = new List<GalleryResponseGallery>();
            foreach (Models.Gallery gallery in galleries)
            {
                Galleries.Add(new GalleryResponseGallery(gallery.ID, gallery.Name, gallery.Password, gallery.Guid, gallery.ShowDownloadAll));
            }
        }
    }

    public class GalleryResponseGallery
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid Guid { get; set; }
        public bool ShowDownloadAll { get; set; }

        public GalleryResponseGallery(int id, string name, string password, Guid guid, bool showDownloadAll)
        {
            ID = id;
            Name = name;
            Password = password;
            Guid = guid;
            ShowDownloadAll = showDownloadAll;
        }
    }
}
