using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspMVC {
  public interface IHttpClientProxy : IDisposable {
    public Task<HttpResponseMessage> GetAsync(string requestUri);
    public Task<HttpResponseMessage> PostAsync(string requestUri, IHttpContentProxy content);
    public Task<HttpResponseMessage> PutAsync(string requestUri, IHttpContentProxy content);
    public Task<HttpResponseMessage> DeleteAsync(string requestUri);
  } 
}
