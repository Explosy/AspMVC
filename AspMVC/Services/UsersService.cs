using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspMVC.Services {
  public class UsersService : IDataService {
    public async Task<IEnumerable<UserDTO>> GetAllUsers() {
      using (HttpClient client = new HttpClient()) {
        using HttpResponseMessage response = await client.GetAsync("https://localhost:44310/users").ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(content);
      }
    }
    public async Task<UserDTO> GetUserById(int id) {
      using (HttpClient client = new HttpClient()) {
        using HttpResponseMessage response = await client.GetAsync($"https://localhost:44310/users/{id}").ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<UserDTO>(content);
      }
    }
    public Task<UserDTO> GetUserByEmail(string email) {
      throw new NotImplementedException();
    }
    public async Task CreateUser(UserDTO userDTO) {
      using (HttpClient client = new HttpClient()) {
        string json = JsonConvert.SerializeObject(userDTO);
        using HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await client.PostAsync($"https://localhost:44310/users/", content).ConfigureAwait(false);
        
      }
    }

  }
}
