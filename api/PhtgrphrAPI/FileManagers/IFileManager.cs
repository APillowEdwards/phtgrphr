using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhtgrphrAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.FileManagers
{
    public interface IFileManager
    {
        FileManagerFile RetrieveImage(Image image);

        bool StoreFile(IFormFile file, string fileName);
    }
}
