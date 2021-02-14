 
namespace AddressResolver.Ip2Country
{
    using System.Net.Http;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Net;
    using System.Threading.Tasks;

    using System;

    public class Ip2CountryClient : AddressResolver
    {
        private readonly static Regex s_regex = new Regex(@"""countryCode"": ""(?<twoCode>[A-Za-z]*)""");
         
        protected override Uri BuildEndPointUri(IPAddress address)
        {
            return new Uri("https://api.ip2country.info/ip?" + address.ToString());
        }

        protected override Task<AddressResolvedResult> ConvertResponseDataAsync(string responseData,
            IPAddress ipAddress)
        {
            var m = s_regex.Match(responseData);
            var result = default(AddressResolvedResult);
            if (m.Groups["twoCode"] is Group g)
            {
                var isReverseAddress = string.IsNullOrWhiteSpace(g.Value);
                var statusCode = isReverseAddress ? StatusCode.Failure : StatusCode.Success;
                var region = isReverseAddress ? RegionInfo.CurrentRegion : new RegionInfo(g.Value);
                result = new AddressResolvedResult(statusCode, isReverseAddress, ipAddress, region);
            }
            else
            {
                result = new AddressResolvedResult(StatusCode.Unknown, true, IPAddress.Any, RegionInfo.CurrentRegion);
            }
            return Task.FromResult(result);
        } 
         
    } 
}
