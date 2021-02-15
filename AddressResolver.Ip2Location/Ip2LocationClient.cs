using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AddressResolver.Utilities;

namespace AddressResolver.Ip2Location
{
    // see https://www.ip2location.com/web-service/ip2location
    public class Ip2LocationClient : AddressResolver
    {
        private readonly string _apiKey;

        public Ip2LocationClient() : this("demo")
        {

        }

        public Ip2LocationClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        protected override Uri BuildEndPointUri(IPAddress address)
        {
            return new Uri(string.Format(
                "https://api.ip2location.com/v2/?ip={0}&key={1}",
                address.ToString(),
                _apiKey));
        }

        protected override Task<AddressResolvedResult> ConvertResponseDataAsync(string responseData,
            IPAddress ipAddress)
        {
            return Task.FromResult(Utils.MakeResultObject(responseData, "country_code", ipAddress));
        }

    }
}