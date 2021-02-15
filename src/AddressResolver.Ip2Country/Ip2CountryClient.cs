 

namespace AddressResolver.Ip2Country
{
    using Utilities;
    using System.Net.Http;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Net;
    using System.Threading.Tasks;

    using System;

    public class Ip2CountryClient : AddressResolver
    {

        protected override Uri BuildEndPointUri(IPAddress address)
        {
            return new Uri("https://api.ip2country.info/ip?" + address.ToString());
        }

        protected override Task<AddressResolvedResult> ConvertResponseDataAsync(string responseData,
            IPAddress ipAddress)
        {
            return Task.FromResult(Utils.MakeResultObject(responseData, "countryCode", ipAddress));
        }

    }


}
