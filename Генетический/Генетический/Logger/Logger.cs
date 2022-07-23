using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генетический.Logger
{
    public class Logger
    {
        public static void log(string message)
        {
            bool activate = false;

            if (activate == true)
                Console.WriteLine(message);
        }
    }
}
