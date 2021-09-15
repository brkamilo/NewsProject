using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsProject.Business;
using NewsProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static NewsProject.Models.Response;
using static NewsProject.Models.ResponseHistory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ServiceSettings ServiceSettings;

        public ValuesController(IOptions<ServiceSettings> serviceSettings)
        {
            if (serviceSettings == null)
            {
                throw new ArgumentNullException(nameof(serviceSettings));
            }
            ServiceSettings = serviceSettings.Value;

        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/GetNews/<ValuesController>
        [HttpPost]
        [Route("GetNews")]
        public JsonResult GetNews([FromBody] string city)
        {
            Response jsonResponse = new Response();
            try
            {
                //Get News Info
                WebRequestAPI webR = new WebRequestAPI();
                ArticlesResult articlesResponse = webR.GetNewsRequest(city, ServiceSettings.APIKeyNews);
                List<News> listNews = new List<News>();
                if (articlesResponse.Status == Statuses.Ok)
                {
                    foreach (var article in articlesResponse.Articles)
                    {
                        News nw = new News()
                        {
                            Author = article.Author,
                            Title = article.Title,
                            Description = article.Description,
                            Url = article.Url,
                            UrlToImage = article.UrlToImage,
                            PublishedAt = (DateTime)article.PublishedAt,
                            Content = article.Content,
                        };
                        listNews.Add(nw);
                    }
                    jsonResponse.news = listNews;
                }

                //Get Weather Info
                string jsonData = webR.GetWeatherRequest(city, ServiceSettings.APIKeyWeather);
                WeatherInfo weatherInfo = new WeatherInfo();
                if (string.IsNullOrEmpty(jsonData) || !jsonData.Contains("Error"))
                {
                    weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(jsonData);
                    Response.Weather wt = new Response.Weather()
                    {
                        Temperature = weatherInfo.Main.Temp,
                        Weather_description = weatherInfo.Weather[0].Description,
                        Wind_speed = weatherInfo.Wind.Speed,
                        Wind_degree = weatherInfo.Wind.Deg,
                        Presure = weatherInfo.Main.Pressure,
                        Humidity = weatherInfo.Main.Humidity
                    };
                    jsonResponse.Current_weather = wt;
                }         
            
            }
            catch (Exception ex)
            {
                jsonResponse.Message = ex.Message;
            }

            return new JsonResult(jsonResponse, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        // POST api/GetNews/<ValuesController>
        [HttpPost]
        [Route("GetHistory")]
        public JsonResult GetHistory()
        {
            ResponseHistory jsonResponse = new ResponseHistory();

            try
            {
                //Get News Info
                WebRequestAPI webR = new WebRequestAPI();          
                List<string> cities = webR.ReadFile();
                List<History> listCity = new List<History>();
                foreach (var item in cities)
                {
                    History his = new History()
                    {
                        City = item
                    };
                    listCity.Add(his);
                }
                jsonResponse.history = listCity;
            }
            catch (Exception ex)
            {
                jsonResponse.Message = ex.Message;
            }

            return new JsonResult(jsonResponse, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
