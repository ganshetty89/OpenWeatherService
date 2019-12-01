using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OpenWeatherService.Libs.Utility;
using System.Net.Http;

namespace OpenWeatherService.Libs.Models
{
    [JsonObject]
    public class WeatherServiceResponse
    {
        [JsonProperty(PropertyName = "coord")]
        public Coordinates Coordinates { get; set; }
        [JsonProperty(PropertyName = "weather")]
        public List<Weather> Weather { get; set; }
        [JsonProperty(PropertyName = "base")]
        public string Base { get; set; }
        [JsonProperty(PropertyName = "main")]
        public Main Main { get; set; }
        [JsonProperty(PropertyName = "visibility")]
        public int Visibility { get; set; }
        [JsonProperty(PropertyName = "wind")]
        public Wind Wind { get; set; }
        [JsonProperty(PropertyName = "clouds")]
        public Clouds Clouds { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("dt")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "sys")]
        public Sys Sys { get; set; }
        [JsonProperty(PropertyName = "timezone")]
        public int TimeZOne { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

    }
    public class Coordinates
    {
        [JsonProperty(PropertyName = "lon")]
        public decimal Longitude { get; set; }
        [JsonProperty(PropertyName = "lat")]
        public decimal Latitude { get; set; }
    }
    public class Weather
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "main")]
        public string Main { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }
    }
    public class Main
    {
        [JsonProperty(PropertyName = "temp")]
        public decimal Temp { get; set; }
        [JsonProperty(PropertyName = "pressure")]
        public int Pressure { get; set; }
        [JsonProperty(PropertyName = "humidity")]
        public int Humidity { get; set; }
        [JsonProperty(PropertyName = "temp_min")]
        public decimal MinTemp { get; set; }
        [JsonProperty(PropertyName = "temp_max")]
        public decimal MaxTemp { get; set; }

    }
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public decimal Speed { get; set; }
        [JsonProperty(PropertyName = "deg")]
        public int Degree { get; set; }
        [JsonProperty(PropertyName = "gust")]
        public decimal Gust { get; set; }
    }
    public class Clouds
    {
        [JsonProperty(PropertyName = "all")]
        public int All { get; set; }
    }
    public class Sys
    {
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("sunrise")]
        public DateTime SunRise { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("sunset")]
        public DateTime SunSet { get; set; }
    }
}
