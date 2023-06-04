using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.classes
{
    /// <summary>
    /// This class is responsibe to log any message to the console. 
    /// </summary>
    public static class ConsoleMessageLogger
    {
        public static void LogErrorToConsole(string errorMessage, bool waitUntilUserPressAnyKey = true)
        {
            Console.ForegroundColor = ConsoleColor.Red; // Cambia el color del texto a amarillo

            Console.WriteLine(errorMessage, ConsoleColor.Red);

            Console.ResetColor();
            if (waitUntilUserPressAnyKey)
            {
                Console.ReadKey();
            }
        }
    }
}
