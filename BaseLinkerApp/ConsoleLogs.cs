using System;
using System.Threading.Tasks;

namespace BaseLinkerApp
{
    class ConsoleLogs : ILogsService
    {
        public void Print(string text)
        {
            Console.WriteLine("[" + DateTime.Now.ToString("G") + "] " + text);
        }
    }
}
