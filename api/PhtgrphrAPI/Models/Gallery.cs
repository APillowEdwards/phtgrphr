using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Models
{
    public class Gallery
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid Guid { get; set; }

        public virtual ICollection<GalleryAccessToken> GalleryAccessTokens { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual User User { get; set; }
    }
}
