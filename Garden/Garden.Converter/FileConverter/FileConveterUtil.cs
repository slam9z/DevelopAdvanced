using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden.Converter
{
    internal static class FileConveterUtil
    {
        public static string GetFileExtention(string path)
        {
            return Path.GetExtension(path);
        }
    }
}
