# Address Resolver
*AddressResolver is a simply, easy to use common IP and Region resolver written in C#.*

## Quick Start
```csharp
// Imports namespace.
using AddressResolver;

// Declares addressResolver object and materialization.
IAddressResolver addressResolver = new Ip2cClient {
    AddressStore = InMemoryAddressStore.Default // options, using default address cache.
};

// Gets the IPAddress and RegionInfo objects
var targetAddress = IPAddress.Parse("<put-your-ip-address-here>");
var result = await addressResolver.ResolveAddressAsync(targetAddress);
```
