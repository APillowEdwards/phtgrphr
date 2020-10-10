using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Models
{
    public class GalleryAccessToken
    {
        public GalleryAccessToken() {}

        public GalleryAccessToken(Gallery gallery, DateTime? expiry = null)
        {
            Token = System.Guid.NewGuid();
            Expiry = expiry ?? DateTime.UtcNow.AddHours(1);
            Gallery = gallery;
        }

        public int ID { get; set; }
        public Guid Token { get; set; }
        public DateTime Expiry { get; set; }

        public virtual Gallery Gallery { get; set; }
    }
}
