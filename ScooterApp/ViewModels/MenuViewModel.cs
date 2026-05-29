using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace ScooterApp.ViewModels
{
    static internal class MenuViewModel
    {
        private static HttpClient _client = new HttpClient();

        public static async Task<string> ChooseWeather()
        {
            try
            {
                string url = "https://api.openweathermap.org/data/2.5/weather?lat=55.3&lon=82.55&appid=063d733994fbedb5cc1aaafbbc6ac9b5";

                HttpResponseMessage response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var res = json.Substring(json.IndexOf("main"), 17);

                    if (await Task.Run(() => res.Contains("Rain") == true))
                    {
                        return "/Images/дождь.png";
                    }
                    else if (await Task.Run(() => res.Contains("Clouds") == true))
                    {
                        return "/Images/облачно.png";
                    }
                    else if (await Task.Run(() => res.Contains("Clear") == true))
                    {
                        return "/Images/солнечно.png";
                    }
                    else
                    {
                        return "/Images/неизвестно.png";
                    }
                }
                return "/Images/неизвестно.png";
            }
            catch
            {
                return "/Images/неизвестно.png";
            }
        }
    }
}
