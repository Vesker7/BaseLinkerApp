using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;

namespace BaseLinkerApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            string metoda = "getOrders";
            string token = "3000897-3004070-XLT2A2AZ8MDQVN1PYL2N1QVNC7W753MFLEKIHYD32CIYJDES7QH33EN88GZ0IINS";
            string address = "https://api.baselinker.com/connector.php";

            WebClient client = new WebClient();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.QueryString.Add("token", token);
            client.QueryString.Add("method", metoda);

            var data = client.UploadValues(address, "POST", client.QueryString);
            JObject reponse = JObject.Parse(Encoding.UTF8.GetString(data));

            Console.WriteLine(reponse);

        }
    }
}
