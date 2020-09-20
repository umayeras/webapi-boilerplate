using System;
using System.IO;

namespace WebApp.Core.Extensions
{
    public static class FileExtensions
    {
        public static bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }
        
        public static void Copy(string sourceFileName, string destinationFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destinationFileName, overwrite);
        }
        
        public static void Delete(string sourceFileName)
        {
            File.Delete(sourceFileName);
        }
        
        public static void ReplaceLocation(string sourceFileName, string destinationFileName, bool overwrite)
        {
            Copy(sourceFileName, destinationFileName, overwrite);
            Delete(sourceFileName);
        }

        public static string CombineFilePath(string folder, string fileName)
        {
            return Path.Combine(folder, fileName);
        }

        public static string GetFileFormat(string fileName)
        {
            return Path.GetExtension(fileName);
        }
        
        public static string GenerateFileName(string fileName)
        {
            var format = GetFileFormat(fileName);
            
            return Guid.NewGuid() + format;
        }

        public static FileStream ReadFile(string path)
        {
            return File.OpenRead(path);
        }
    }
}