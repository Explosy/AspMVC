using AspMVC.Models;
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

    public async Task<ResponseModel<IEnumerable<UserDTO>>> GetItems(string email) {
      using (IHttpClientProxy client = httpClientProxy()) {
        using HttpResponseMessage response = await client.GetAsync($"{settings.ApiAddress}?Email={email}").ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<ResponseModel<IEnumerable<UserDTO>>>(content);
      }
    }
    public async Task<ResponseModel<UserDTO>> GetItemById(int id) {
      using (IHttpClientProxy client = httpClientProxy()) {
        using HttpResponseMessage response = await client.GetAsync($"{settings.ApiAddress}{id}").ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject< ResponseModel<UserDTO>>(content);
      }
    }
    public async Task<ResponseModel<UserDTO>> CreateItem(UserDTO userDTO) {
      ResponseModel<UserDTO> model = new ResponseModel<UserDTO>() {
        Data = userDTO
      };
      using (IHttpClientProxy client = httpClientProxy()) {
        string json = JsonConvert.SerializeObject(model);
        using IHttpContentProxy content = httpContentProxy(json);
        using HttpResponseMessage response = await client.PostAsync(settings.ApiAddress, content).ConfigureAwait(false);
        string res_content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<ResponseModel<UserDTO>>(res_content);
      }
    }
    public async Task<ResponseModel<UserDTO>> UpdateItem(UserDTO userDTO) {
      ResponseModel<UserDTO> model = new ResponseModel<UserDTO>() {
        Data = userDTO
      };
      using (IHttpClientProxy client = httpClientProxy()) {
        string json = JsonConvert.SerializeObject(model);
        using IHttpContentProxy content = httpContentProxy(json);
        using HttpResponseMessage response = await client.PutAsync($"{settings.ApiAddress}{userDTO.Id}", content).ConfigureAwait(false);
        string res_content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<ResponseModel<UserDTO>>(res_content);
      }
    }
    public async Task<ResponseModel<UserDTO>> DeleteItem(int id) {
      using (IHttpClientProxy client = httpClientProxy()) {
        using HttpResponseMessage response = await client.DeleteAsync($"{settings.ApiAddress}{id}").ConfigureAwait(false);
        string res_content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<ResponseModel<UserDTO>>(res_content);
      }
    }
  }
}
