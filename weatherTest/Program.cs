using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace weatherTest
{

    class Program
    {

        static async Task Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                string? apiKey = Environment.GetEnvironmentVariable("API_KEY");

                GeoAPI geoAPI = new GeoAPI(client, apiKey);
                WeatherAPI weatherAPI = new WeatherAPI(client, apiKey);

                Console.Write("Enter your city: ");
                string? cityName = Console.ReadLine();

                if (string.IsNullOrEmpty(cityName))
                {
                    Console.WriteLine("City name need contain minimum 1 letter");
                    return;
                }

                var coordinates = await geoAPI.GetCoordinates(cityName);
                if (coordinates != null)
                {
                    double temper = await weatherAPI.GetWeather(coordinates);
                    Console.WriteLine($"{temper:F2}");
                }
                else
                {
                    Console.WriteLine("No city found");
                }
            }
        }
    }
}