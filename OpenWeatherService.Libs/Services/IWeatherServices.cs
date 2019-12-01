using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherService.Libs.Services
{
    public interface IWeatherServices
    {
        Task<HttpResponseMessage> ReadAndStoreWeatherDataToFile(string cityFilePath, string outputFolderPath);
    }
}
