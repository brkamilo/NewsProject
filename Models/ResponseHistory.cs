using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Models
{
    public class ResponseHistory
    {
        public List<History> history { get; set; }

        public class History
        {
            public string City { get; set; }
        }

        public string Message { get; set; }

    }
}
