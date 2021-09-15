using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Models
{
    public class WeatherInfo
    {
        public List<Weather> Weather{ get; set; }
        public Main Main{ get; set; }
        public Wind Wind{ get; set; }
    }

    public class Weather
    { 
        public string Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class Main
    { 
        public string Temp { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }

    }

    public class Wind
    {
        public string Speed { get; set; }
        public string Deg { get; set; }

    }
}
