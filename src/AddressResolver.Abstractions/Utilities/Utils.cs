using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace AddressResolver.Utilities
{
    public static class Utils
    {
        public static bool TryResolveFromJsonProperty(string json, string jsonPropertyName, out string iso3166)
        {
            var m = Regex.Match(json, @"""" + jsonPropertyName + @""": ""(?<ISO3166>[A-Za-z]*)""",
                RegexOptions.Compiled | RegexOptions.ExplicitCapture);
            var flag = m.Success;
            iso3166 = flag ? m.Groups["ISO3166"].Value : string.Empty;
            return flag;
        }

        public static AddressResolvedResult MakeResultObject(string json, string jsonPropertyName, IPAddress ipAddress)
        { 
            var result = default(AddressResolvedResult);
            var flag = TryResolveFromJsonProperty(json, "countryCode", out var iso3166);
            if (flag)
            {
                var isReverseAddress = string.IsNullOrWhiteSpace(iso3166);
                var statusCode = isReverseAddress ? StatusCode.Failure : StatusCode.Success;
                var region = isReverseAddress ? RegionInfo.CurrentRegion : new RegionInfo(iso3166);
                result = new AddressResolvedResult(statusCode, isReverseAddress, ipAddress, region);
            }
            else
            {
                result = new AddressResolvedResult(StatusCode.Unknown, true, IPAddress.Any, RegionInfo.CurrentRegion);
            }

            return result;
        }
    }
}