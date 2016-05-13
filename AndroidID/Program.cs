using Idnator.Helpers;
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
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Idnator";
            string path = "";

            if (args.Length == 0)
            {
                Console.WriteLine("Insert path to android layout file or directory containing layouts (.xml or .axml): ");
                path = Console.ReadLine();
            }
            else
            {
                path = args[0];
                Console.WriteLine("Input path: " + path);
            }

            if (FileHelper.IsDirecotry(path))
                FileHelper.BeautifyFiles(path);
            else
                FileHelper.BeautifyFile(path);
              
            Console.ReadKey();
        }
    }
}
