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
    public Task<HttpResponseMessage> PostAsync(string requestUri, IHttpContentProxy content) {
      return httpClient.PostAsync(requestUri, content.Content);
    }
    public Task<HttpResponseMessage> PutAsync(string requestUri, IHttpContentProxy content) {
      return httpClient.PutAsync(requestUri, content.Content);
    }
    public Task<HttpResponseMessage> DeleteAsync(string requestUri) {
      return httpClient.DeleteAsync(requestUri);
    }
    public void Dispose() {
      httpClient.Dispose();
    }
  }
}
