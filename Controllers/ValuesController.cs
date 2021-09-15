using Microsoft.AspNetCore.Mvc;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsProject.Business;
using NewsProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NewsProject.Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
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
        public JsonResult News([FromBody] string city)
        {
            Response jsonResponse = new Response();
            try
            {
                //Get News Info
                WebRequestAPI webR = new WebRequestAPI();
                string APIKey = "4e5a1de8a79143eaa7ebada9204e9240";
                ArticlesResult articlesResponse = webR.GetNewsRequest(city, APIKey);
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
                APIKey = "f126c09b9974b8344b2087f863f459c4";          
                string jsonData = webR.GetWeatherRequest(city, APIKey);
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
