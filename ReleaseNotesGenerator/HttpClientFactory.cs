using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ReleaseNotesGenerator
{
    public sealed class HttpClientFactory : IDisposable
    {
        private readonly ConcurrentDictionary<Uri, HttpClient> _httpClients;

        public HttpClientFactory()
        {
            _httpClients = new ConcurrentDictionary<Uri, HttpClient>();
        }

        public HttpClient Create(Uri baseAddress)
        {
            return _httpClients.GetOrAdd(baseAddress,
                b => new HttpClient { BaseAddress = b, DefaultRequestHeaders = { Accept = { new MediaTypeWithQualityHeaderValue("application/json") }}});
        }

        public void Dispose()
        {
            foreach (var httpClient in _httpClients.Values)
            {
                httpClient.Dispose();
            }
        }
    }
}