using System;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BaseLinkerApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            BaseLinkerClient client = new("3000897-3004070-XLT2A2AZ8MDQVN1PYL2N1QVNC7W753MFLEKIHYD32CIYJDES7QH33EN88GZ0IINS", new FileLogs("appLogs.txt"));

            /*
            var data = client.UploadValues(address, "POST", client.QueryString);
            JObject reponse = JObject.Parse(Encoding.UTF8.GetString(data));

            Console.WriteLine(reponse);
            */

        }
    }
}
