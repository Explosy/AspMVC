using Microsoft.Extensions.Configuration;

namespace AspMVC {
  public class Settings : ISettings {
    IConfiguration config { get; set; }
    public Settings(IConfiguration config) {
      this.config = config;
    }
    public string ApiAddress { get {
        return config.GetValue<string>("ApiAddress");
      } 
    }
  }
}
