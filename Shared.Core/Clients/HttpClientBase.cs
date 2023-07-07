using Newtonsoft.Json;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Shared.Core.Clients
{
    public abstract class HttpClientBase
    {
        private readonly string _baseUrl;

        public HttpClientBase(string baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        public async Task<R> Post<R, P>(string url, P payload, CancellationToken cancellationToken = default)
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.PostAsJsonAsync(url, payload, cancellationToken);

            return await ParseHttpResponseAsync<R>(response);
        }

        protected async Task<R> Get<R>(string url, CancellationToken cancellationToken = default)
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.GetAsync(url, cancellationToken);

            return await ParseHttpResponseAsync<R>(response);
        }

        protected async Task Get(string url, CancellationToken cancellationToken = default)
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.GetAsync(url, cancellationToken);

            await HandleHttpResponseAsync(response);
        }

        protected async Task<R> Put<R, P>(string url, P payload, CancellationToken cancellationToken = default)
        {
            var httpClient = GetHttpClient();

            var response = await httpClient.PutAsJsonAsync(url, payload, cancellationToken);

            return await ParseHttpResponseAsync<R>(response);
        }

        private async Task HandleHttpResponseAsync(HttpResponseMessage response)
        {
            response.ThrowIfNull();

            var responseContentString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ClientUnexpectedErrorException(
                    "Error occurred while processing http request.",
                    response.StatusCode,
                    responseContentString);
            }
        }

        private async Task<R> ParseHttpResponseAsync<R>(HttpResponseMessage response)
        {
            response.ThrowIfNull();

            var responseContentString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ClientUnexpectedErrorException(
                    "Error occurred while processing http request.",
                    response.StatusCode,
                    responseContentString);
            }

            var responseContent = JsonConvert.DeserializeObject<R>(responseContentString);
            if (responseContent == null)
            {
                throw new ClientUnexpectedErrorException(
                    "Unable to parse response.",
                    System.Net.HttpStatusCode.InternalServerError,
                    responseContentString);
            }

            return responseContent;
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_baseUrl),
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
