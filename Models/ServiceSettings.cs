using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Models
{
    public class ServiceSettings
    {
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public string APIKeyNews { get; set; }
        public string APIKeyWeather { get; set; }
    }
}
