using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportDistance.Configurations
{
    public class AirportApiOptions
    {
        public const string Section = "AirportAPI";
        public string BaseUrl { get; set; }
        public int TimeOutMinutes { get; set; }
    }
}
