using System.Linq;
using System.Threading.Tasks;
using AspMVC.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace AspMVCProject.Controllers {
  public class UsersController : Controller {
    private readonly UsersService usersService;
    public UsersController(UsersService usersService) {
      this.usersService = usersService;
    }

    public async Task<IActionResult> Index() {
      return View(await usersService.GetAllUsers());
    }

    //public async Task<IActionResult> Find(string email) {
    //  return View(await _context.Users.Where(u => u.Email.Contains(email)).ToListAsync());
    //}

    public async Task<IActionResult> Details(int? id) {
      if (id == null) {
        return NotFound();
      }
      UserDTO user = await usersService.GetUserById((int)id);
      if (user == null) {
        return NotFound();
      }
      return View(user);
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Surname,Age,Email,RegistationDate")] UserDTO user) {
      if (ModelState.IsValid) {
        await usersService.CreateUser(user);
        return RedirectToAction(nameof(Index));
      }
      return View(user);
    }

    //public async Task<IActionResult> Edit(int? id) {
    //  if (id == null) {
    //    return NotFound();
    //  }

    //  User user = await _context.Users.FindAsync(id);
    //  if (user == null) {
    //    return NotFound();
    //  }
    //  return View(user);
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Age,Email,RegistationDate")] User user) {
    //  if (id != user.Id) {
    //    return NotFound();
    //  }

    //  if (ModelState.IsValid) {
    //    try {
    //      _context.Update(user);
    //      await _context.SaveChangesAsync();
    //    } catch (DbUpdateConcurrencyException) {
    //      if (!UserExists(user.Id)) {
    //        return NotFound();
    //      } else {
    //        throw;
    //      }
    //    }
    //    return RedirectToAction(nameof(Index));
    //  }
    //  return View(user);
    //}

    //public async Task<IActionResult> Delete(int? id) {
    //  if (id == null) {
    //    return NotFound();
    //  }

    //  User user = await _context.Users
    //      .FirstOrDefaultAsync(m => m.Id == id);
    //  if (user == null) {
    //    return NotFound();
    //  }

    //  return View(user);
    //}

    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(int id) {
    //  User user = await _context.Users.FindAsync(id);
    //  _context.Users.Remove(user);
    //  await _context.SaveChangesAsync();
    //  return RedirectToAction(nameof(Index));
    //}

    //private bool UserExists(int id) {
    //  return _context.Users.Any(e => e.Id == id);
    //}
  }
}
