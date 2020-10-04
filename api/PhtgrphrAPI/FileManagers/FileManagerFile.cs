using System.IO;

namespace PhtgrphrAPI.FileManagers
{
    public class FileManagerFile
    {
        public FileStream File;
        public string MimeType;

        public FileManagerFile(FileStream file, string mimeType)
        {
            File = file;
            MimeType = mimeType;
        }
    }
}