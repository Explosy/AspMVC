using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspMVC.Services {
  public class UsersService : IDataService<UserDTO> {
    private readonly ISettings settings;
    private readonly Func<IHttpClientProxy> httpClientProxy;
    private readonly Func<string,IHttpContentProxy> httpContentProxy;
    public UsersService(ISettings settings, Func<IHttpClientProxy> httpClientProxy, Func<string, IHttpContentProxy> httpContentProxy) { 
      this.settings = settings;
      this.httpClientProxy = httpClientProxy;
      this.httpContentProxy = httpContentProxy;
    }
    public async Task<IEnumerable<UserDTO>> GetAllItems() {
      using (IHttpClientProxy client = httpClientProxy()) {
        using HttpResponseMessage response = await client.GetAsync(settings.ApiAddress).ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(content);
      }
    }
    public async Task<IEnumerable<UserDTO>> FindItemsByProperty(string email) {
      using (IHttpClientProxy client = httpClientProxy()) {
        using HttpResponseMessage response = await client.GetAsync($"{settings.ApiAddress}?Email={email}").ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(content);
      }
    }
    public async Task<UserDTO> GetItemById(int id) {
      using (IHttpClientProxy client = httpClientProxy()) {
        using HttpResponseMessage response = await client.GetAsync($"{settings.ApiAddress}{id}").ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<UserDTO>(content);
      }
    }
    public async Task<bool> CreateItem(UserDTO userDTO) {
      using (IHttpClientProxy client = httpClientProxy()) {
        string json = JsonConvert.SerializeObject(userDTO);
        using IHttpContentProxy content = httpContentProxy(json);
        using HttpResponseMessage response = await client.PostAsync(settings.ApiAddress, content).ConfigureAwait(false);
        if (response.IsSuccessStatusCode) {
          return true;
        }
        return false;
      }
    }
    public async Task<bool> UpdateItem(UserDTO userDTO) {
      using (IHttpClientProxy client = httpClientProxy()) {
        string json = JsonConvert.SerializeObject(userDTO);
        using IHttpContentProxy content = httpContentProxy(json);
        using HttpResponseMessage response = await client.PutAsync($"{settings.ApiAddress}{userDTO.Id}", content).ConfigureAwait(false);
        if (response.IsSuccessStatusCode) {
          return true;
        }
        return false;
      }
    }
    public async Task<bool> DeleteItem(int id) {
      using (IHttpClientProxy client = httpClientProxy()) {
        using HttpResponseMessage response = await client.DeleteAsync($"{settings.ApiAddress}{id}").ConfigureAwait(false);
        if (response.IsSuccessStatusCode) {
          return true;
        }
        return false;
      }
    }
  }
}
