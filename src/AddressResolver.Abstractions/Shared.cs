using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


using System.Collections.Concurrent;
using System.Net;

namespace AddressResolver
{
    public static class Shared
    {
        public static IHttpClientFactory HttpClientFactory { get; } = new InternalHttpClientFactory();

        public static IAddressStore AddressStore { get; } = new InMemoryAddressStore();

        private sealed class InternalHttpClientFactory : IHttpClientFactory
        {
            private readonly static Lazy<HttpClient> s_httpClient = new Lazy<HttpClient>(() => new HttpClient());

            public HttpClient GetHttpClient()
            {
                return s_httpClient.Value;
            }
        }

        private sealed class InMemoryAddressStore : IAddressStore
        {
            private readonly ConcurrentDictionary<IPAddress, AddressResolvedResult> _cache =
                new ConcurrentDictionary<IPAddress, AddressResolvedResult>();

            public InMemoryAddressStore()
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
}