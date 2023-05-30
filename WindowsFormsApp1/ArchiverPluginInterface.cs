using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public interface PluginInterface
    {
        string FileExt { get; }
        void Archive(FileStream fileStream);
        void Dearchive(MemoryStream fileStream);
    }
}
