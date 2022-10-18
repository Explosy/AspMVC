using AspAPI.Models;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAPI.Controllers {
  [Route("/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase {

    UsersDBContext dBContext { get; set; }
    public UsersController(UsersDBContext dBContext) {
      this.dBContext = dBContext;
    }

    [HttpGet]
    public async Task<ResponseModel<IEnumerable<User>>> Get([FromQuery(Name = "Email")] string email) {
      IEnumerable<User> users;
      if (email == null) {
        users = await dBContext.Users.ToListAsync();
      } else {
        users = await dBContext.Users.Where(u => u.Email.Contains(email)).ToListAsync();
      }
      ResponseModel<IEnumerable<User>> response = new ResponseModel<IEnumerable<User>>() {
        Data = users
      };
      return response;
    }

    [HttpGet("{id}")]
    public async Task<ResponseModel<User>> Get(int id) {
      User user = await dBContext.Users
          .FirstOrDefaultAsync(m => m.Id == id);
      if (user == null) {
        return new ResponseModel<User>() {
          IsSuccess = false,
          Error = "Пользователь с указанным id не найден"
        };
      }
      return new ResponseModel<User>() {
        Data = user
      };
    }

    [HttpPost]
    public async Task<ResponseModel<User>> Post(User user) {
      if (user == null) {
        return new ResponseModel<User>() {
          IsSuccess = false,
          Error = "Переданный пользователь - NULL"
        };
      }
      dBContext.Users.Add(user);
      await dBContext.SaveChangesAsync();
      return new ResponseModel<User>() {
        Data = user
      };
    }

    [HttpPut("{id}")]
    public async Task<ResponseModel<User>> Put(User user) {
      if (user == null) {
        return new ResponseModel<User>() {
          IsSuccess = false,
          Error = "Переданный пользователь - NULL"
        };
      }
      if (!dBContext.Users.Any(x => x.Id == user.Id)) {
        return new ResponseModel<User>() {
          IsSuccess = false,
          Error = "Пользователь с указанным id не найден"
        };
      }
      dBContext.Update(user);
      await dBContext.SaveChangesAsync();
      return new ResponseModel<User>() {
        Data = user
      };
    }

    [HttpDelete("{id}")]
    public async Task<ResponseModel<User>> DeleteConfirmed(int id) {
      User user = dBContext.Users.FirstOrDefault(x => x.Id == id);
      if (user == null) {
        return new ResponseModel<User>() {
          IsSuccess = false,
          Error = "Пользователь с указанным id не найден"
        };
      }
      dBContext.Users.Remove(user);
      await dBContext.SaveChangesAsync();
      return new ResponseModel<User>() {
        Data = user
      };
    }
  }
}
