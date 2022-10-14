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
    public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content) {
      return httpClient.PostAsync(requestUri, content);
    }
    public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content) {
      return httpClient.PutAsync(requestUri, content);
    }
    public Task<HttpResponseMessage> DeleteAsync(string requestUri) {
      return httpClient.DeleteAsync(requestUri);
    }
    public void Dispose() {
      httpClient.Dispose();
    }

    
  }
}
