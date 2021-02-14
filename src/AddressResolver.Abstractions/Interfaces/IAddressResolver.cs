using System.Net;
using System.Threading.Tasks;

namespace AddressResolver
{
    public interface IAddressResolver
    {
        IAddressStore AddressStore { get; }

        Task<AddressResolvedResult> ResolveAddressAsync(string ipString);

        Task<AddressResolvedResult> ResolveAddressAsync(IPAddress ipAddress);
    }
}