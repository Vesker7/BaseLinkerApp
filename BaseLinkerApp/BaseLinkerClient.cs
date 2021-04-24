using System.Net;
using System.Text;
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
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.QueryString.Add("token", Token);
            client.QueryString.Add("method", "");
            client.QueryString.Add("parameters", "");
        }

        private void ChooseMethod(string method)
        {
            client.QueryString.Set("method", method);
        }

        public JObject GetOrders()
        {
            _logService.Print("Pobieranie zamówień.");
            ChooseMethod("getOrders");

            var data = client.UploadValues(Address, "POST", client.QueryString);
            JObject response = JObject.Parse(Encoding.UTF8.GetString(data));

            return response;
        }

        public JObject GetOrder(int order_id)
        {
            JObject param = new JObject();
            param.Add("order_id", order_id);

            _logService.Print("Pobieranie zamówienia o numerze: " + order_id + ".");
            ChooseMethod("getOrders");
            client.QueryString.Set("parameters", param.ToString());

            var data = client.UploadValues(Address, "POST", client.QueryString);
            JObject response = JObject.Parse(Encoding.UTF8.GetString(data));

            return response;
        }

        public JObject GetStoragesList()
        {
            _logService.Print("Pobieranie dostępnych magazynów.");
            ChooseMethod("getStoragesList");

            var data = client.UploadValues(Address, "POST", client.QueryString);
            JObject response = JObject.Parse(Encoding.UTF8.GetString(data));

            return response;
        }

        public JObject GetCategories(string storage_id)
        {
            JObject param = new JObject();
            param.Add("storage_id", storage_id);

            _logService.Print("Pobieranie kategorii z magazynu: " + storage_id + ".");
            ChooseMethod("getCategories");
            client.QueryString.Set("parameters", param.ToString());

            var data = client.UploadValues(Address, "POST", client.QueryString);
            JObject response = JObject.Parse(Encoding.UTF8.GetString(data));

            return response;
        }

        public JObject GetProductsList(string storage_id)
        {
            JObject param = new JObject();
            param.Add("storage_id", storage_id);

            _logService.Print("Pobieranie produktów z magazynu: " + storage_id + ".");
            ChooseMethod("getProductsList");
            client.QueryString.Set("parameters", param.ToString());

            var data = client.UploadValues(Address, "POST", client.QueryString);
            JObject response = JObject.Parse(Encoding.UTF8.GetString(data));

            return response;
        }

        public JObject GetProduct(string storage_id, string filter_name)
        {
            JObject param = new JObject();
            param.Add("storage_id", storage_id);
            param.Add("filter_name", filter_name);

            _logService.Print("Pobieranie produktu o nazwie: " + filter_name + " z magazynu: " + storage_id + ".");
            ChooseMethod("getProductsList");
            client.QueryString.Set("parameters", param.ToString());

            var data = client.UploadValues(Address, "POST", client.QueryString);
            JObject response = JObject.Parse(Encoding.UTF8.GetString(data));

            return response;
        }

        public JObject AddProduct(string storage_id, string category_id, string productName, int stockQuantity, float priceBrutto, int taxRate)
        {
            JObject param = new JObject();
            param.Add("storage_id", storage_id);
            param.Add("name", productName);
            param.Add("category_id", category_id);
            param.Add("quantity", stockQuantity);
            param.Add("price_brutto", priceBrutto);
            param.Add("tax_rate", taxRate);

            _logService.Print("Dodawanie produktu o nazwie: " + productName + " stan magazynowy: " + stockQuantity + " cena brutto: " + priceBrutto + " VAT: " + taxRate + "%.");
            ChooseMethod("addProduct");
            client.QueryString.Set("parameters", param.ToString());

            var data = client.UploadValues(Address, "POST", client.QueryString);
            JObject response = JObject.Parse(Encoding.UTF8.GetString(data));

            return response;
        }

        public JObject AddOrderWithGift(int order_id)
        {
            JObject order = GetOrder(order_id);

            if(!order["orders"].HasValues)
            {
                _logService.Print("Tworzenie zamówienia na podstawie zamówienia nr: " + order_id + " nie powiodło się, gdyż nie istnieje zamówienie o takim numerze!");
                return new JObject();
            }

            string[] paramsToDelete = { "order_id", "shop_order_id", "external_order_id", "order_source", "order_source_id", "order_source_info", "confirmed", "date_confirmed", "date_in_status",
                                        "payment_done", "delivery_package_module", "delivery_package_nr", "order_page", "pick_state", "pack_state", "delivery_country", "invoice_country" };

            foreach (string param in paramsToDelete)
            {
                order["orders"].First[param].Parent.Remove();
            }

            order["orders"].First["admin_comments"] = "Zamówienie utworzone na podstawie " + order_id;

            // TODO: DODAĆ GRATIS

            _logService.Print("Tworzenie zamówienia na podstawie zamówienia nr: " + order_id + ".");
            ChooseMethod("addOrder");
            client.QueryString.Set("parameters", order["orders"].First.ToString());

            var data = client.UploadValues(Address, "POST", client.QueryString);
            JObject response = JObject.Parse(Encoding.UTF8.GetString(data));

            return response;
        }
    }
}
