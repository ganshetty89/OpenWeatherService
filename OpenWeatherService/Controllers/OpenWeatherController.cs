using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OpenWeatherService.Libs.Services;

namespace OpenWeatherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenWeatherController : ControllerBase
    {
        IWeatherServices _weatherServices;
        IHostingEnvironment _hostingenv;
        public OpenWeatherController(IWeatherServices weatherServices, IHostingEnvironment hostingenv)
        {
            _weatherServices = weatherServices;
            _hostingenv = hostingenv;
        }
        [HttpGet]
        [Route("V1/ReadAndStoreWeatherDataToFile")]
        public async Task<HttpResponseMessage> Get()
        {
            var webroot = _hostingenv.WebRootPath;
            var cityFilePath = System.IO.Path.Combine(webroot, "Cities\\CityList.csv");
            var outputPath = System.IO.Path.Combine(webroot, "Output");
            return await _weatherServices.ReadAndStoreWeatherDataToFile(cityFilePath, outputPath);
         }
    }
}
