using System;
using System.Net.Http;
using SmartWallet.DAL.Entity;

namespace SmartWallet.AL.API;

public class ApiEngine
{
    private static string _rateApiKey = "fxr_live_4462ef8b75e6631cc97d6553776f4fe2a3bf";

    private HttpClient _client { get; set; }

    public ApiEngine()
    {
        _client = new HttpClient();
    }
    
    public string getRateJson(Currency from, Currency to, DateTime date)
    {
        string url = $"https://api.fxratesapi.com/convert?api_key={_rateApiKey}&from={from.ToString()}&to={to.ToString()}&date={date.ToString("yyyy-MM-dd")}&amount=1&format=json";
        var response = _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        var content = response.Result.Content.ReadAsStringAsync();
        return content.Result;
    }
}