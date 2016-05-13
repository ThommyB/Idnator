using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idnator
{
    public static class ColoredConsole
    {
        public static void WriteLine(object @object, ConsoleColor textColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = textColor;
            Console.WriteLine(@object.ToString().PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }

        public static void WriteException(Exception ex)
        {
            WriteLine(ex.Message, ConsoleColor.Red);
#if DEBUG
            WriteLine(ex.ToString(), ConsoleColor.Red);
#endif
        }
    }
}
