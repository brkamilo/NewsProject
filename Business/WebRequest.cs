using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace NewsProject.Business
{
    public class WebRequestAPI
    {
        public ArticlesResult GetNewsRequest(string city, string APIKey)
        {
            var newsApiClient = new NewsApiClient(APIKey);
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = city,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2021, 8, 25)
            });
            SaveFile(city);
            return articlesResponse;
        }

        public string GetWeatherRequest(string city, string APIKey)
        {
            var url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", city, APIKey);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            string responseBody = string.Empty;

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd();

                        }
                    }
                }
                return responseBody;
            }
            catch (WebException ex)
            {
                return "Error " + ex.Message;
            }
        }
        public void SaveFile(string line)
        {
            var path = Path.Combine("history", "history.txt");
            if (!Directory.Exists("History"))
            {
                Directory.CreateDirectory("History");
            }
            if (!File.Exists(path))
            {
                 File.Create(path);
            }

            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                sw.WriteLineAsync(line);
            }
        }

        public List<string> ReadFile()
        {
            List<string> Cities = new List<string>();
            var path = Path.Combine("history", "history.txt");
            using (StreamReader sr = File.OpenText(path))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    Cities.Add(line);
                }
            }
            return Cities;

        }


    }
}
