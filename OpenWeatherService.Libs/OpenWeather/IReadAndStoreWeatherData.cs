using OpenWeatherService.Libs.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherService.Libs.OpenWeather
{
    public interface IReadAndStoreWeatherData
    {
        Task<HttpResponseMessage> ReturnCityWeatherBasedOnCityId(string cityId);
        Task<HttpResponseMessage> ReadAndStoreWeatherDataToFile(string cityFilePath, string outputFolderPath);
    }
}
