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
using PhtgrphrAPI.Responses;

namespace PhtgrphrAPI.FileManagers
{
    public class AzureBlobFileManager : IFileManager
    {
        private string ConnectionString;
        private static Dictionary<string, byte[]> Cache = new Dictionary<string, byte[]>();

        public AzureBlobFileManager(IConfiguration configuration)
        {
            ConnectionString = configuration["AzureStorageConnectionString"];
        }

        public AzureBlobFileManager(String connectionString)
        {
            ConnectionString = connectionString;
        }

        public ResponseFile RetrieveImage(Image image)
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

            // If the cache doesn't contain the image, we need to get it first
            if (!Cache.ContainsKey(image.FileName))
            {

                // Get a reference to the file
                BlobServiceClient serviceClient = new BlobServiceClient(ConnectionString);
                BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(image.FileName);

                // Download the file
                Stream stream = blobClient.OpenRead();

                MemoryStream memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                // Double-check it's not been added already in the interim
                if (!Cache.ContainsKey(image.FileName))
                {
                    Cache.Add(image.FileName, memoryStream.ToArray());
                }
            }

            MemoryStream cacheMemoryStream = new MemoryStream(Cache[image.FileName]);

            return new ResponseFile(cacheMemoryStream, image.FileName, mimeType);
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

        public bool DeleteImage(Image image)
        {
            string containerName = "phtgrphr-images";

            try
            {
                // Get a reference to the new file
                BlobServiceClient serviceClient = new BlobServiceClient(ConnectionString);
                BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(image.FileName);

                blobClient.Delete();

                return true;
            }
            catch
            {
                return false;
            }

            throw new NotImplementedException();
        }
    }
}
