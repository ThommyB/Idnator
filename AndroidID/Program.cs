using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AndroidID
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Idnator ---");
            Console.WriteLine("Paste in path to layouts: ");
            string path = Console.ReadLine();

            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path, "*.axml"))
                {
                    FileAttributes attributes = File.GetAttributes(file);

                    if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
                        File.SetAttributes(file, attributes);
                        Console.WriteLine("The {0} file is no longer RO.", file);
                    }

                    File.WriteAllText(file, XmlHelper.AddIds(file), Encoding.Unicode);

                    File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.ReadOnly);
                    Console.WriteLine("The {0} file is now RO.", file);
                }
            }
            Console.ReadKey();
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }
    }
}
