using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace BaseLinkerApp
{
    internal class BaseLinkerClient
    {
        private ILogsService _logService;
        private static readonly string Address = "https://api.baselinker.com/connector.php";
        private readonly string Token;
        private WebClient client = new WebClient();

        public BaseLinkerClient(string token, ILogsService logsOutput)
        {
            Token = token;
            _logService = logsOutput;
        }

        private void ApplyRequestBody(string method)
        {
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.QueryString.Add("token", Token);
            client.QueryString.Add("method", method);
        }

        // TODO: ADD API METHODS: getProducts, addProduct, getOrders, getOrder, addOrder, 
    }
}
