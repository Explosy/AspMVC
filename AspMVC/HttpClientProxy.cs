using System.Net.Http;
using System.Threading.Tasks;

namespace AspMVC {
  public class HttpClientProxy : IHttpClientProxy {
    private readonly HttpClient httpClient;
    public HttpClientProxy(HttpClient httpClient) {
      this.httpClient = httpClient;
    }
    public Task<HttpResponseMessage> GetAsync(string requestUri) {
      return httpClient.GetAsync(requestUri);
    }
    public void Dispose() {
      httpClient.Dispose();
    }
  }
}
