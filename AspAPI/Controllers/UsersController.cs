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
    public async Task<ActionResult<IEnumerable<User>>> Get([FromQuery(Name ="Email")]string email) {
      if (email == null) {
        return await dBContext.Users.ToListAsync();
      }
      return await dBContext.Users.Where(u => u.Email.Contains(email)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id) {
      User user = await dBContext.Users
          .FirstOrDefaultAsync(m => m.Id == id);
      if (user == null) {
        return NotFound();
      } 
      return new ObjectResult(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user) {
      if (user == null) {
        return BadRequest();
      }
      dBContext.Users.Add(user);
      await dBContext.SaveChangesAsync();
      return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Put(User user) {
      if (user == null) {
        return BadRequest();
      }
      if (!dBContext.Users.Any(x => x.Id == user.Id)) {
        return NotFound();
      }
      dBContext.Update(user);
      await dBContext.SaveChangesAsync();
      return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      User user = dBContext.Users.FirstOrDefault(x => x.Id == id);
      if (user == null) {
        return NotFound();
      }
      dBContext.Users.Remove(user);
      await dBContext.SaveChangesAsync();
      return Ok(user);
    }
  }
}
