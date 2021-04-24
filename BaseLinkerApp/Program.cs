using System;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Ninject;

namespace BaseLinkerApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            ILogsService _logsService;
            Console.WriteLine("Wybierz miejsce wypisywania logów (0 - konsola, 1 - plik): ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Wybrano wypisywanie logów do pliku.");
                Console.WriteLine("Podaj ścieżkę do pliku:");
                string path = Console.ReadLine();
                _logsService = new FileLogs("appLogs.txt");
            }
            else if (choice == "0")
            {
                Console.WriteLine("Wybrano wypisywanie logów w konsoli.");
                _logsService = new ConsoleLogs();
            }
            else
            {
                Console.WriteLine("Błędny wybór! Domyślne ustawienie: Wypisywanie logów w konsoli.");
                _logsService = new ConsoleLogs();
            }
           
            BaseLinkerClient client = new("3000897-3004070-XLT2A2AZ8MDQVN1PYL2N1QVNC7W753MFLEKIHYD32CIYJDES7QH33EN88GZ0IINS", _logsService);

            Console.WriteLine(client.AddOrderWithGift(4839622));
        }
    }
}
