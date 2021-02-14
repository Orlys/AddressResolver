using System;
using System.Net;
using System.Threading.Tasks;

namespace AddressResolver.UnitTest
{
    public class MockAddressResolver : IAddressResolver
    { 
        public IAddressStore AddressStore { get; }
        public virtual Task<AddressResolvedResult> ResolveAddressAsync(string ipString)
        {
            throw new NotImplementedException();
        }

        public virtual Task<AddressResolvedResult> ResolveAddressAsync(IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}