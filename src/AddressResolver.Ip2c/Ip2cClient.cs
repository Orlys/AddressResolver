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

        public override Uri EndPoint { get; } = new Uri("https://ip2c.org");

        public override bool SupportedV4 => true;

        protected override Task<AddressResolvedResult> ConvertResponseDataAsync(string responseData,
            IPAddress ipAddress)
        {
            var perpetratedData = responseData.Split(';');
            var statusCode = (StatusCode)(perpetratedData[0][0] - 0x30);

            var twoLetterCode = perpetratedData[1];
            var isReversed = s_reversed.Any(x => string.Equals(x, twoLetterCode));
            var result = new AddressResolvedResult(statusCode, isReversed, ipAddress,
                isReversed ? RegionInfo.CurrentRegion : new RegionInfo(twoLetterCode));

            return Task.FromResult(result);
        }
    }
}