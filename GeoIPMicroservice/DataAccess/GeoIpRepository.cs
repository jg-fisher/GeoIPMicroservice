using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GeoIPMicroservice.Services;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Model;
using Microsoft.Extensions.Logging;

namespace GeoIPMicroservice.DataAccess
{
    public class GeoIpRepository
    {
        private readonly ILogger<GeoIpRepository> _logger;

        private readonly string _dbFilePath = Path.Join(Directory.GetCurrentDirectory(), "GeoLite2-Country.mmdb");

        public GeoIpRepository(ILogger<GeoIpRepository> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Determines the Country location of a given IP address.
        /// </summary>
        /// <param name="ipAddress">string IPv4 IP address</param>
        /// <returns>string Country that the IPv4 address belongs to</returns>
        public Country GetCountryMapping(string ipAddress)
        {
            try
            {
                using (var reader = new DatabaseReader(_dbFilePath))
                {
                    var country = reader.Country(ipAddress);
                    return country.Country;
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.ToString());
                return null;
            }
        }
    }
}
