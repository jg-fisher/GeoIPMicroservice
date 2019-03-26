using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GeoIPMicroservice.DataAccess;
using GeoIPMicroservice.Models;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Model;
using Microsoft.Extensions.Logging;

namespace GeoIPMicroservice.Services
{
    public class GeoIpService
    {
        private readonly GeoIpRepository _geoIpRepository;

        public GeoIpService(GeoIpRepository geoIpRepository)
        {
            _geoIpRepository = geoIpRepository;
        }

        /// <summary>
        /// Determines an IP address maps to a given list of countries.
        /// </summary>
        /// <returns>True or False whether the IPv4 Address is from a whitelisted country.</returns>
        public bool? IsIsoWhitelisted(GeoIpRequest geoIpRequest)
        {
            Country countryMapping = _geoIpRepository.GetCountryMapping(geoIpRequest.IpAddress);
            if (countryMapping != null) 
            {
                return geoIpRequest.WhitelistedCountries.Contains(countryMapping.IsoCode);
            }
            return false;
        }
    }
}
