using System;
using System.Net.Http;

namespace AspMVC {
  public interface IHttpContentProxy : IDisposable {
    public HttpContent Content { get; set; }
  }
}
