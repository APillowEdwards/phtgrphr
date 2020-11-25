using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Responses
{
    public class ImageResponse
    {
        public ImageResponseImage Image { get; set; }

        public ImageResponse(Models.Image image)
        {
            Image = new ImageResponseImage(image.ID, image.Sort, image.Visible);
        }
    }
    public class ImageListResponse
    {
        public List<ImageResponseImage> Images { get; set; }
        public int TotalCount { get; set; }

        public ImageListResponse(List<Models.Image> images, int totalCount)
        {
            Images = new List<ImageResponseImage>();
            foreach (Models.Image image in images)
            {
                Images.Add(new ImageResponseImage(image.ID, image.Sort, image.Visible));
            }

            TotalCount = totalCount;
        }
    }

    public class ImageResponseImage
    {
        public int ID { get; set; }
        public int Sort { get; set; }
        public bool Visible { get; set; }

        public ImageResponseImage(int id, int sort, bool visible)
        {
            ID = id;
            Sort = sort;
            Visible = visible;
        }
    }
}
