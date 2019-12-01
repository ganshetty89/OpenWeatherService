using OpenWeatherService.Libs.OpenWeather;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherService.Libs.Services
{
    public class WeatherService : IWeatherServices
    {
        IReadAndStoreWeatherData _readAndStoreWeatherData;
        public WeatherService(IReadAndStoreWeatherData readAndStoreWeatherData)
        {
            _readAndStoreWeatherData = readAndStoreWeatherData;
        }
        public async Task<HttpResponseMessage> ReadAndStoreWeatherDataToFile(string cityFilePath, string outputFolderPath)
        {
            return await _readAndStoreWeatherData.ReadAndStoreWeatherDataToFile(cityFilePath, outputFolderPath);
        }
    }
}
