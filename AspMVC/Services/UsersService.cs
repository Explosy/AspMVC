using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspMVC.Services {
  internal class UsersService : IDataService {
    public async Task<IEnumerable<UserDTO>> GetAllUsers() {
      using (HttpClient client = new HttpClient()) {
        using HttpResponseMessage response = await client.GetAsync("https://localhost:44310/users").ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(content);
      }
    }

    public async Task<UserDTO> GetUser(int id) {
      using (HttpClient client = new HttpClient()) {
        using HttpResponseMessage response = await client.GetAsync($"https://localhost:44310/users/{id}").ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<UserDTO>(content);
      }
    }
  }
}
