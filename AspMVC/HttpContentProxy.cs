using System.Net.Http;
using System.Text;

namespace AspMVC {
  public class HttpContentProxy  : IHttpContentProxy {
    public HttpContent Content { get; set; }
    public HttpContentProxy(string json) {
      this.Content = new StringContent(json, Encoding.UTF8, "application/json");
    }

    public void Dispose() {
      Content.Dispose();
    }
  }
}
