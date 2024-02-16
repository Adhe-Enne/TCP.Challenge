using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Framework
{
    public static class IO
    {
        public static string MoveFile(string FileName, string ToFolder, string ToFileName = null)
        {
            if (ToFileName == null)
                ToFileName = Path.GetFileName(FileName);
            else
                ToFileName = Path.GetFileName(ToFileName);

            ToFolder = ToFolderNormalize(ToFolder);

            if (!Directory.Exists(ToFolder))
                Directory.CreateDirectory(ToFolder);

            string destFileName = Path.Combine(ToFolder, ToFileName);

            File.Copy(FileName, destFileName, true);

            File.Delete(FileName);

            return destFileName;
        }

        public static string ToFolderNormalize(string ToFolder)
        {
            string monthFolder = DateTime.Today.ToString("yyyy-MM");

            ToFolder = Path.Combine(ToFolder, monthFolder);

            if (!Directory.Exists(ToFolder))
                Directory.CreateDirectory(ToFolder);

            return ToFolder;
        }

        public static void SaveFile(string path, string name, string data, string extension)
        {
            string fileName = $"{name}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.{extension}";

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string normalizedFolder = Core.Framework.IO.ToFolderNormalize(path);
            string fullFileName = (Path.Combine(normalizedFolder, fileName));
            File.AppendText(fullFileName).NewLine=data;
        }

        public static void SaveFile(string path, string name, string data)
        {
            string fileName = $"{name}.txt";

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            
            string normalizedFolder = Core.Framework.IO.ToFolderNormalize(path);
            string fullFileName = (Path.Combine(normalizedFolder, fileName));
            File.AppendAllLines(fullFileName, new string[] { data });
        }
    }
}