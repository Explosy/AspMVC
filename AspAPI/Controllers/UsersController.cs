using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    public async Task<ActionResult<IEnumerable<User>>> Get() {
      return await dBContext.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id) {
      User user = await dBContext.Users
          .FirstOrDefaultAsync(m => m.Id == id);
      if (user == null)
        return NotFound();
      return user;
    }

    [HttpPost]
    public void Post([FromBody] string value) {
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value) {
    }

    [HttpDelete("{id}")]
    public void Delete(int id) {
    }
  }
}
