using Azure;
using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhtgrphrAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Azure.Storage.Blobs;

namespace PhtgrphrAPI.FileManagers
{
    public class AzureBlobFileManager : IFileManager
    {
        private string ConnectionString;

        public AzureBlobFileManager(IConfiguration configuration)
        {
            ConnectionString = configuration["AzureStorageConnectionString"];
        }

        public AzureBlobFileManager(String connectionString)
        {
            ConnectionString = connectionString;
        }

        public FileManagerFile RetrieveImage(Image image)
        {
            string containerName = "phtgrphr-images";

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
            BlobServiceClient serviceClient = new BlobServiceClient(ConnectionString);
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(image.FileName);

            // Download the file
            Stream stream = blobClient.OpenRead();

            return new FileManagerFile(stream, mimeType);
        }

        public bool StoreFile(IFormFile file, string fileName)
        {
            string containerName = "phtgrphr-images";

            try
            {
                // Get a reference to the new file
                BlobServiceClient serviceClient = new BlobServiceClient(ConnectionString);
                BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                using (Stream stream = file.OpenReadStream())
                {
                    blobClient.Upload(stream);
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
