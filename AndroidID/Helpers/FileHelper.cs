using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idnator.Helpers
{
    public static class FileHelper
    {
        public const string EXTENSIONS = "*.axml|*.xml";

        public static void BeautifyFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    FileRemoveROAttribute(path);

                    File.WriteAllText(path, XmlHelper.AddIds(path), Encoding.Unicode);
                    
                    ColoredConsole.WriteLine(string.Format("{0} SUCCESSFULLY generated.", path), ConsoleColor.Green);
                }
                catch (Exception ex)
                {
                    ColoredConsole.WriteLine(string.Format("{0} file generation has failed!\n{1}", path, ex.Message), ConsoleColor.Red);
                }
            }
            else
                ColoredConsole.WriteLine(string.Format("{0} - NOT FOUND!", path), ConsoleColor.Red);
        }

        public static void BeautifyFiles(string directory)
        {
            if (Directory.Exists(directory))
            {
                foreach (string file in GetFiles(directory, EXTENSIONS))
                {
                    BeautifyFile(file);
                }
            }
            else
                ColoredConsole.WriteLine("Directory NOT FOUND!", ConsoleColor.Red);
        }

        public static void FileRemoveROAttribute(string path)
        {
            if (File.Exists(path))
            {
                FileAttributes attributes = File.GetAttributes(path);

                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
                    File.SetAttributes(path, attributes);
                }
            }
        }

        /// <summary>
        /// Checks whether given path is file or directory
        /// </summary>
        /// <param name="path"></param>
        /// <returns>true if given path is directory</returns>
        public static bool IsDirecotry(string path)
        {
            FileAttributes attrs = File.GetAttributes(path);

            return attrs.HasFlag(FileAttributes.Directory);
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        /// <summary>
        /// Retrieves all files in given directory, filtered by specific pattern
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="searchPattern">Search pattern to filter returned files. For example "*.axml|*.xml" </param>
        /// <returns></returns>
        private static string[] GetFiles(string directory, string searchPattern)
        {
            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            foreach (string sp in searchPatterns)
                files.AddRange(Directory.GetFiles(directory, sp));
            files.Sort();

            return files.ToArray();
        }
    }
}
