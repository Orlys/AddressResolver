namespace AddressResolver
{
    using System.Collections.Concurrent;
    using System.Net;

    public sealed class InMemoryAddressStore : IAddressStore
    {
        public readonly static IAddressStore Default = new InMemoryAddressStore();

        private readonly ConcurrentDictionary<IPAddress, AddressResolvedResult> _cache = new ConcurrentDictionary<IPAddress, AddressResolvedResult>();

        private InMemoryAddressStore()
        {
        }

        public void AddOrSet(IPAddress ipAddress, AddressResolvedResult result)
        {
            _cache.AddOrUpdate(ipAddress, a => result, (a, r) => result);
        }

        public void Clear()
        {
            _cache.Clear();
        }

        public bool TryGet(IPAddress ipAddress, out AddressResolvedResult result)
        {
            return _cache.TryGetValue(ipAddress, out result);
        }
    }
}