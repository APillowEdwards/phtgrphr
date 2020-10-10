using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
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
    public class AzureFilesManager : IFileManager
    {
        private string ConnectionString;

        public AzureFilesManager(IConfiguration configuration)
        {
            ConnectionString = configuration["AzureStorageConnectionString"];
        }

        public AzureFilesManager(String connectionString)
        {
            ConnectionString = connectionString;
        }

        public FileManagerFile RetrieveImage(Image image)
        {
            string dirName = "images";
            string shareName = "phtgrphr-images";

            string mimeType;
            // Only supporting .jpeg and .png at the moment
            switch (Path.GetExtension(image.FileName))
            {
                case ".jpg":
                case ".jpeg":
                    mimeType = "image/jpeg";
                    break;
                case ".png":
                    mimeType = "image/png";
                    break;
                default:
                    throw new NotImplementedException("Cannot serve file with extension: " + Path.GetExtension(image.FileName));
            }

            // Get a reference to the file
            ShareClient share = new ShareClient(ConnectionString, shareName);
            ShareDirectoryClient directory = share.GetDirectoryClient(dirName);
            ShareFileClient fileClient = directory.GetFileClient(image.FileName);

            // Download the file
            Stream stream = fileClient.OpenRead();
            
            return new FileManagerFile(stream, mimeType);
        }

        public bool StoreFile(IFormFile file, string fileName)
        {
            string dirName = "images";
            string shareName = "phtgrphr-images";

            try
            {
                // Get a reference to a share and create it
                ShareClient share = new ShareClient(ConnectionString, shareName);
                share.CreateAsync();

                // Get a reference to a directory and create it
                ShareDirectoryClient directory = share.GetDirectoryClient(dirName);
                directory.CreateAsync();

                // Get a reference to a file and upload it
                ShareFileClient fileClient = directory.GetFileClient(fileName);
                using (Stream stream = file.OpenReadStream())
                {
                    fileClient.Create(stream.Length);
                    fileClient.UploadRange(new HttpRange(0, stream.Length), stream);
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
