using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Idnator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Idnator";
            Console.WriteLine("--- Idnator ---");
            string path = "";

            if (args.Length == 0)
            {
                Console.WriteLine("Paste in path to layouts: ");
                path = Console.ReadLine();
            }
            else
            {
                path = args[0];
                Console.WriteLine("Input path: " + path);
            }

            if (Directory.Exists(path))
            {
                foreach (string file in GetFiles(path, "*.axml|*.xml"))
                {
                    try
                    {
                        FileAttributes attributes = File.GetAttributes(file);

                        if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        {
                            attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
                            File.SetAttributes(file, attributes);
                        }

                        File.WriteAllText(file, XmlHelper.AddIds(file), Encoding.Unicode);

                        File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.ReadOnly);
                        Console.WriteLine("{0} succesfully generated.", file);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("{0} file generation has failed!\n{1}", file, ex.Message);
                    }
                }
            }
            else
                Console.WriteLine("Directory not found!");
              
            Console.ReadKey();
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        private static string[] GetFiles(string path, string searchPattern)
        {
            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            foreach (string sp in searchPatterns)
                files.AddRange(Directory.GetFiles(path, sp));
            files.Sort();

            return files.ToArray();
        }
    }
}
