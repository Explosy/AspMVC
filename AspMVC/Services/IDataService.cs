using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC.Services {
  interface IDataService {
    Task<IEnumerable<UserDTO>> GetAllUsers();
    Task<UserDTO> GetUserById(int id);
    Task<UserDTO> GetUserByEmail(string email);
    Task CreateUser(UserDTO userDTO);
    
  }
}
