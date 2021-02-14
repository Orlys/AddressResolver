using System.Net;

namespace AddressResolver
{
    public interface IAddressStore
    {
        void AddOrSet(IPAddress ipAddress, AddressResolvedResult result);

        void Clear();

        bool TryGet(IPAddress ipAddress, out AddressResolvedResult result);
    }
}