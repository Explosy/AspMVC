using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspMVC {
  public interface IHttpClientProxy : IDisposable {
    public Task<HttpResponseMessage> GetAsync(string requestUri);
  } 
}
