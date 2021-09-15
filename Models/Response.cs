using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Models
{
    public class Response
    {
        public List<News> news { get; set; }
        public Weather Current_weather{ get; set; }


        public class News
        { 
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }

        }

        public class Weather
        {
            public string Temperature { get; set; }
            public string Weather_description { get; set; }
            public string Wind_speed { get; set; }
            public string Wind_degree { get; set; }
            public string Presure { get; set; }
            public string Humidity { get; set; }
        }
        public string Message { get; set; }

     
    }
}
