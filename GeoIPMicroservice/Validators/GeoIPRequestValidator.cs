using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GeoIPMicroservice.Models;

namespace GeoIPMicroservice.Validators
{
    public class GeoIpRequestValidator : AbstractValidator<GeoIpRequest>
    {
        public GeoIpRequestValidator()
        {
            RuleFor(x => x.IpAddress)
                .Must(BeValidIpv4Address)
                .WithMessage("Please specify a valid Ipv4 address.");

            RuleFor(x => x.WhitelistedCountries)
                .Must(BeValidIsoCodes)
                .WithMessage("Whitelisted country codes must be in ISO format. " +
                             "For more information please see: https://dev.maxmind.com/geoip/legacy/codes/");
        }

        /// <summary>
        /// Validates that a string is a valid IPv4 address.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private bool BeValidIpv4Address(string ipAddress)
        {
            if (String.IsNullOrWhiteSpace(ipAddress))
            {
                return false;
            }

            string[] splitValues = ipAddress.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;
            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        /// <summary>
        /// Validates that each country in a string[] is ISO code format by length.
        /// </summary>
        /// <param name="countries"></param>
        /// <returns>True if all countries are in ISO code format, false otherwise.</returns>
        private bool BeValidIsoCodes(string[] countries)
        {
            IEnumerable<string> invalidIsoCodes = countries.Where(c => c.Length > 2);
            return !invalidIsoCodes.Any() ? true : false;
        }
    }
}
