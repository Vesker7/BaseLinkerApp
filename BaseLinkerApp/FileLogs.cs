using System;
using System.IO;

namespace BaseLinkerApp
{
    class FileLogs : ILogsService
    {
        private StreamWriter OutputFile = null;

        public FileLogs(string outputFilePath)
        {
            OutputFile = new StreamWriter(outputFilePath, append: true);
        }

        public async void Print(string text)
        {
            try
            {
                using (OutputFile)
                {
                    await OutputFile.WriteLineAsync("[" + DateTime.Now.ToString("G") + "] " + text);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
