using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using GeoIPMicroservice.Models;
using GeoIPMicroservice.Services;
using GeoIPMicroservice.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeoIPMicroservice.Controllers
{
    [Route("api/[controller]")]
    public class GeoIpController : ControllerBase
    {
        private readonly GeoIpService _geoIpService;

        public GeoIpController(GeoIpService geoIpService)
        {
            _geoIpService = geoIpService;
        }

        /// <summary>
        /// Determines whether an IP address is within a list of countries.
        /// </summary>
        /// <param name="geoIpRequest">GeoIpRequest JSON object</param>
        /// <returns>Boolean whether an IP address is from a whitelisted country.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] GeoIpRequest geoIpRequest)
        {
                    return Ok(_geoIpService.IsIsoWhitelisted(geoIpRequest));
        }
    }
}
