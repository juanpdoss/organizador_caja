using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.classes
{
    internal static class Logger
    {
        static string PATH_TO_LOG_TXT = "log.txt";

        public static void Log(Exception e)
        {
            using (StreamWriter file = new StreamWriter(PATH_TO_LOG_TXT, append: true))
            {
                file.WriteLine("Log date: " + DateTime.Now);
                file.WriteLine("Exception name: ", e.GetType().Name);
                file.WriteLine(e.Message);
                file.WriteLine("---- end of log ----");
            }

        }

    }
}
