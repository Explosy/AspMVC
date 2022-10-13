using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC.Services {
  interface IDataService {
    Task<IEnumerable<UserDTO>> GetAllUsers();
    Task<UserDTO> GetUserById(int id);
    Task<IEnumerable<UserDTO>> FindUsersByEmail(string email);
    Task CreateUser(UserDTO userDTO);
    Task UpdateUser(UserDTO userDTO);
    Task DeleteUser(int id);
  }
}
