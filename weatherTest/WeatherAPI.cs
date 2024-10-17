using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherTest
{
    class WeatherAPI
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public WeatherAPI(HttpClient client, string apiKey)
        {
            _client = client;
            _apiKey = apiKey;
        }

        public async Task<double> GetWeather(GeocodingResponse coordinates)
        {

            var endpointFinal = $@"https://api.openweathermap.org/data/2.5/weather?lat={coordinates.Lat}&lon={coordinates.Lon}&appid={_apiKey}&units=metric";

            var responseFinal = await _client.GetAsync(endpointFinal);

            if (responseFinal.IsSuccessStatusCode)
            {

                var resultFinal = await responseFinal.Content.ReadAsStringAsync();

                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(resultFinal);

                return weatherData.Main.Temp;
            }

            return double.NaN;
        }
    }
}
