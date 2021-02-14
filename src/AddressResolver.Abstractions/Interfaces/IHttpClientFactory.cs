using System.Net.Http;

namespace AddressResolver
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}