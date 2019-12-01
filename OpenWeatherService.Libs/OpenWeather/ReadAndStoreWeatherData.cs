using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenWeatherService.Libs.Models;

namespace OpenWeatherService.Libs.OpenWeather
{

    public class ReadAndStoreWeatherData : IReadAndStoreWeatherData
    {
        private IConfiguration _config;
        public ReadAndStoreWeatherData(IConfiguration config)
        {
            _config = config;
        }
        public async Task<HttpResponseMessage> ReturnCityWeatherBasedOnCityId(string cityId)
        {
            string apiKey = _config.GetSection("apiKey").Value;

            using (var client = new HttpClient())
            {
                var url = new Uri($"https://api.openweathermap.org/data/2.5/weather?id={cityId}&appid={apiKey}");

                return await client.GetAsync(url);
            }
        }

        public async Task<HttpResponseMessage> ReadAndStoreWeatherDataToFile(string cityFilePath, string outputFolderPath)
        {
            using (StreamReader sr = new StreamReader(cityFilePath))
            {
                //Csv reader reads the stream
                CsvReader csvread = new CsvReader(sr);

                //csvread will fetch all record in one go to the IEnumerable object record
                IEnumerable<City> cities = csvread.GetRecords<City>();

                foreach (var city in cities) // Each record will be fetched and printed on the screen
                {
                    HttpResponseMessage response = await ReturnCityWeatherBasedOnCityId(city.CityId);

                    string json;
                    using (var content = response.Content)
                    {
                        json = await content.ReadAsStringAsync();
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        WeatherServiceResponse weatherServiceResponse = JsonConvert.DeserializeObject<WeatherServiceResponse>(json);
                        var path = System.IO.Path.Combine(outputFolderPath, weatherServiceResponse.Date.ToString("ddMMyyyy") + "" + weatherServiceResponse.Name + ".txt");
                        using (StreamWriter writer = new StreamWriter(path, false))
                        {
                            writer.WriteLine("City Id: " + weatherServiceResponse.Id);
                            writer.WriteLine("City Name: " + weatherServiceResponse.Name);
                            writer.WriteLine("Coordinates: ");
                            writer.WriteLine("   Latitude: " + weatherServiceResponse.Coordinates.Latitude);
                            writer.WriteLine("   Longitude: " + weatherServiceResponse.Coordinates.Longitude);
                            writer.WriteLine("Weather:");
                            foreach (var items in weatherServiceResponse.Weather.Select((value, index) => new { value, index }))
                            {
                                writer.WriteLine("  Weather " + items.index + 1);
                                writer.WriteLine("    Id:" + items.value.Id);
                                writer.WriteLine("    Main:" + items.value.Main);
                                writer.WriteLine("    Description:" + items.value.Description);
                                writer.WriteLine("    Icon:" + items.value.Icon);
                            }
                            writer.WriteLine("Base:" + weatherServiceResponse.Base);
                            writer.WriteLine("Main:");
                            writer.WriteLine("  Temp:" + weatherServiceResponse.Main.Temp);
                            writer.WriteLine("  Pressure:" + weatherServiceResponse.Main.Pressure);
                            writer.WriteLine("  Humidity:" + weatherServiceResponse.Main.Humidity);
                            writer.WriteLine("  Minimum Temp:" + weatherServiceResponse.Main.MinTemp);
                            writer.WriteLine("  Maximum Temp:" + weatherServiceResponse.Main.MaxTemp);
                            writer.WriteLine("Visibility:" + weatherServiceResponse.Visibility);
                            writer.WriteLine("Wind:");
                            writer.WriteLine("  Speed:" + weatherServiceResponse.Wind.Speed);
                            writer.WriteLine("  Degree:" + weatherServiceResponse.Wind.Degree);
                            writer.WriteLine("Clouds:");
                            writer.WriteLine("  All:" + weatherServiceResponse.Clouds.All);
                            writer.WriteLine("Sys:");
                            writer.WriteLine("  Type:" + weatherServiceResponse.Sys.Type);
                            writer.WriteLine("  Id:" + weatherServiceResponse.Sys.Id);
                            writer.WriteLine("  Country:" + weatherServiceResponse.Sys.Country);
                            writer.WriteLine("  Sunrise:" + weatherServiceResponse.Sys.SunRise);
                            writer.WriteLine("  Sunset:" + weatherServiceResponse.Sys.SunSet);
                        }
                    }
                    else
                    {
                        throw new Exception("Error Response from Open Weather service");
                    }

                }
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
