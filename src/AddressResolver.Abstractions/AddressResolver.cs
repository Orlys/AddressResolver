namespace AddressResolver
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public abstract class AddressResolver : IAddressResolver, IDisposable
    {
        private readonly HttpClient _httpClient;

        public IAddressStore AddressStore { get; set; }

        public abstract Uri EndPoint { get; }
        public virtual bool SupportedAddressStore => this.AddressStore != null;
        public virtual bool SupportedV4 { get; } = false;
        public virtual bool SupportedV6 { get; } = false;

        protected AddressResolver() : this(null)
        {
        }

        protected AddressResolver(HttpMessageHandler handler)
        {
            this._httpClient = new HttpClient(handler ?? new HttpClientHandler()) { BaseAddress = EndPoint };
        }

        public void Dispose()
        {
            this._httpClient.Dispose();
        }

        public virtual Task<AddressResolvedResult> ResolveAddressAsync(string ipString)
        {
            if (string.IsNullOrWhiteSpace(ipString))
            {
                throw new ArgumentException("ip can not be null, empty or whitespace", nameof(ipString));
            }

            return this.ResolveAddressAsync(IPAddress.Parse(ipString));
        }

        public virtual Task<AddressResolvedResult> ResolveAddressAsync(IPAddress ipAddress)
        {
            if (ipAddress is null)
            {
                throw new ArgumentNullException(nameof(ipAddress));
            }

            return this.ResolveAddressCore(ipAddress);
        }

        protected abstract Task<AddressResolvedResult> ConvertResponseDataAsync(string responseData, IPAddress ipAddress);

        private async Task<AddressResolvedResult> ResolveAddressCore(IPAddress ipAddress)
        {
            Debug.Assert(ipAddress != null);

            switch (ipAddress.AddressFamily)
            {
                case AddressFamily.InterNetwork when (this.SupportedV4):
                case AddressFamily.InterNetworkV6 when (this.SupportedV6):
                    {
                        break;
                    }
                default:
                    throw new NotSupportedException("Not supported protocol.");
            }

            var ipString = ipAddress.ToString();
            if (this.SupportedAddressStore && this.AddressStore.TryGet(ipAddress, out var result))
            {
                return result;
            }
            else
            {
                var responseContent = await this._httpClient.GetStringAsync("/" + ipString).ConfigureAwait(false); //
                result = await ConvertResponseDataAsync(responseContent, ipAddress);

                if (this.SupportedAddressStore)
                    this.AddressStore.AddOrSet(ipAddress, result);

                return result;
            }
        }
    }
}