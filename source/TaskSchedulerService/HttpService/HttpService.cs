using System.Net.Http;
using System.Threading.Tasks;

namespace TaskSchedulerService
{
    public sealed class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task SendAsync(string method, string url)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), url);

            return _httpClientFactory.CreateClient().SendAsync(request);
        }
    }
}
