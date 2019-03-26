using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoIPMicroservice.Models
{
    public class GeoIpRequest
    {
        public string IpAddress { get; set; }

        public string[] WhitelistedCountries { get; set; }
    }
}
