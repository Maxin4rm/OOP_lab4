using System.IO;
using System.IO.Compression;
using System.IO.Pipes;

namespace ZipArchiverPlugin
{
    public class ZipFileArchiver : WindowsFormsApp1.PluginInterface
    {
        public string FileExt => ".zip";

        public void Archive(FileStream fileStream)
        {
            string originalFile = Path.GetFileNameWithoutExtension(fileStream.Name);
            fileStream.Position = 0;
            using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                ZipArchiveEntry entry = archive.CreateEntry(originalFile);
                using (Stream entryStream = entry.Open())
                {
                    fileStream.CopyTo(entryStream);
                }
            }
        }

        

        public void Dearchive(MemoryStream archiveStream)
        {
            using (ZipArchive archive = new ZipArchive(archiveStream, ZipArchiveMode.Read, true))
            {
                ZipArchiveEntry entry = archive.Entries[0];
                using (Stream stream = entry.Open())
                {
                    archiveStream.Position = 0;
                    archiveStream.SetLength(entry.Length);
                    stream.CopyTo(archiveStream);
                }
                archiveStream.Position = 0;
            }
        }
    }
}
