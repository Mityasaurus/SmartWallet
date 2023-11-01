using SmartWallet.DAL.Entity;
using SmartWallet.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartWallet.Aplication.API
{
    public class ApiEngineCrypto
    {
        private HttpClient _client { get; set; }

        public ApiEngineCrypto()
        {
            _client = new HttpClient();
        }

        public string getRateJson(Currency from, Currency to)
        {
            string url = $"https://api.coingecko.com/api/v3/simple/price?ids={MoneyProvider.CryptoNames[from].ToString()}&vs_currencies={to.ToString()}";
            var response = _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            var content = response.Result.Content.ReadAsStringAsync();
            return content.Result;
        }
    }
}
