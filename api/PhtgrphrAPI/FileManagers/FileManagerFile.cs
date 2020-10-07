using System.IO;

namespace PhtgrphrAPI.FileManagers
{
    public class FileManagerFile
    {
        public Stream File;
        public string MimeType;

        public FileManagerFile(Stream file, string mimeType)
        {
            File = file;
            MimeType = mimeType;
        }
    }
}