using System.Net.Http;

namespace AddressResolver.Ip2c
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    public class Ip2cClient : AddressResolver
    {
        private readonly static string[] s_reversed = { null, string.Empty, "ZZ" };
         
         
        protected override Task<AddressResolvedResult> ConvertResponseDataAsync(string responseData,
            IPAddress ipAddress)
        {
            var perpetratedData = responseData.Split(';');
            var statusCode = (StatusCode)(perpetratedData[0][0] - 0x30);

            var twoLetterCode = perpetratedData[1];
            var isReversed = s_reversed.Contains(twoLetterCode);
            var result = new AddressResolvedResult(statusCode, isReversed, ipAddress,
                isReversed ? RegionInfo.CurrentRegion : new RegionInfo(twoLetterCode));

            return Task.FromResult(result);
        }

        protected override Uri BuildEndPointUri(IPAddress address)
        { 
            return new Uri("https://ip2c.org/" + address.ToString());
        } 
    }
}