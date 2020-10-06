using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhtgrphrAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.FileManagers
{
    public class LocalFileManager : IFileManager
    {
        private IConfiguration Configuration;

        public LocalFileManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public FileManagerFile RetrieveImage(Image image)
        {
            string imagePath = Configuration["ImagePath"] + image.FileName;

            var imageFile = System.IO.File.OpenRead(imagePath);

            string mimeType;
            // Only supporting .jpeg and .png at the moment
            switch (Path.GetExtension(imagePath))
            {
                case ".jpg":
                case ".jpeg":
                    mimeType = "image/jpeg";
                    break;
                case ".png":
                    mimeType = "image/png";
                    break;
                default:
                    throw new NotImplementedException("Cannot serve file with extension: " + Path.GetExtension(imagePath));
            }

            return new FileManagerFile(imageFile, mimeType);
        }

        public bool StoreFile(IFormFile file, string fileName)
        {
            try
            {

                string imageStorePath = Configuration["ImagePath"] + fileName;

                using (Stream stream = new FileStream(imageStorePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
