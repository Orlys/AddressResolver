 
using NUnit.Framework;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Moq;

namespace AddressResolver.UnitTest
{
    public class FunctionTests
    {
        private IAddressResolver _addressResolver;

        [SetUp]
        public void Setup()
        {
            var result = new AddressResolvedResult(
                StatusCode.Success,
                true,
                IPAddress.Loopback,
                RegionInfo.CurrentRegion); 

            var moq = new Mock<MockAddressResolver>();
            moq.Setup(x => x.ResolveAddressAsync(result.Address)).ReturnsAsync(() => result);
   
            this._addressResolver = moq.Object;
        }

        [Test()]
        public async Task ResolveAddressAsync__GetSelfAddress_EnsureAddressResolved()
        {
            var feed = IPAddress.Loopback;
            var expected = new AddressResolvedResult(
                StatusCode.Success,
                true,
                IPAddress.Loopback,
                RegionInfo.CurrentRegion);

            var actual = await _addressResolver.ResolveAddressAsync(feed); 

            Assert.AreEqual(expected, actual);
        }
    }
}