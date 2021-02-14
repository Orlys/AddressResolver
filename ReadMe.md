# Address Resolver
*AddressResolver is a simply, easy to use common IP and Region resolver written in C#.*

## Quick Start
```csharp
// Imports namespace.
using AddressResolver;

// Declares addressResolver object and materialization.
IAddressResolver addressResolver = new Ip2cClient {
    AddressStore = Shared.AddressStore // options, using default address cache.
};

// Gets the IPAddress and RegionInfo objects
var targetAddress = IPAddress.Parse("8.8.8.8");
var result = await addressResolver.ResolveAddressAsync(targetAddress);
/*
    result:
        - Address: 8.8.8.8 (type: IPAddress)
        - IsReverseAddress: false (type: bool)
        - RegionInfo: US (type: RegionInfo, see https://docs.microsoft.com/en-us/dotnet/api/system.globalization.regioninfo) 
        - StatusCode: StatusCode.Success (type: StatusCode)
*/
```

## License
**Apache 2.0**