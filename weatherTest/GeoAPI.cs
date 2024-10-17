using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherTest
{
    class GeoAPI
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public GeoAPI(HttpClient client, string apiKey)
        {
            _client = client;
            _apiKey = apiKey;
        }

        public async Task<GeocodingResponse?> GetCoordinates(string cityName)
        {

            var endpointGEO = @$"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid={_apiKey}";

            var responseGEO = await _client.GetAsync(endpointGEO);

            if (responseGEO.IsSuccessStatusCode)
            {
                string resultGEO = await responseGEO.Content.ReadAsStringAsync();
                List<GeocodingResponse>? geocodingResponses = JsonConvert.DeserializeObject<List<GeocodingResponse>>(resultGEO);
                return geocodingResponses[0];
            }

            return null;
        }
    }
}
