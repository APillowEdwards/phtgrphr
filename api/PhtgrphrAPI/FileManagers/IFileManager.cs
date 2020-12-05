using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhtgrphrAPI.Models;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.FileManagers
{
    public interface IFileManager
    {
        ResponseFile RetrieveImage(Image image);

        bool StoreFile(IFormFile file, string fileName);

        bool DeleteImage(Image image);
    }
}
