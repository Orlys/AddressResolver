using System;
using System.Threading.Tasks;
using AddressResolver.Ip2Country;

namespace AddressResolver.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var addressResolver = new Ip2CountryClient();
            var result = await addressResolver.ResolveAddressAsync("1.1.1.1");

        }
    }
}
