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
    public class AwsS3FileManager : IFileManager
    {
        private IConfiguration Configuration;

        public AwsS3FileManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public FileManagerFile RetrieveImage(Image image)
        {
            throw new NotImplementedException();
        }

        public bool StoreFile(IFormFile file, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
