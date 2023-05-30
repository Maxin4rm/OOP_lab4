using System.IO.Compression;
using System.IO;

namespace GZipArchiverPlugin
{
    public class GZFileArchiver : WindowsFormsApp1.PluginInterface
    {

        public string FileExt => ".gz";

        public void Archive(FileStream fileStream)
        {                
            fileStream.Seek(0, SeekOrigin.Begin);
            using (MemoryStream compressedStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
                {
                    fileStream.CopyTo(gzipStream);
                }
                fileStream.Position = 0;
                fileStream.SetLength(compressedStream.Length);
                compressedStream.Position = 0;
                compressedStream.CopyTo(fileStream);
            }
        }


        public void Dearchive(MemoryStream archiveStream)
        {
            archiveStream.Position = 0;
            using (GZipStream gzip = new GZipStream(archiveStream, CompressionMode.Decompress, leaveOpen: true))
            {
                MemoryStream unpackedStream = new MemoryStream();
                gzip.CopyTo(unpackedStream);
                unpackedStream.Position = 0;
                archiveStream.SetLength(unpackedStream.Length);
                archiveStream.Position = 0;
                unpackedStream.CopyTo(archiveStream);
            }
            archiveStream.Position = 0;   
        }
    }  
}
