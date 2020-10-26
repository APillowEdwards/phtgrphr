using System.IO;

namespace PhtgrphrAPI.Responses
{
    public class ResponseFile
    {
        public Stream File;
        public string Name;
        public string MimeType;

        public ResponseFile(Stream file, string name, string mimeType)
        {
            File = file;
            Name = name;
            MimeType = mimeType;
        }
    }
}