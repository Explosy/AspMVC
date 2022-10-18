using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspMVC.Models;
using AspMVC.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace AspMVCProject.Controllers {
  public class UsersController : Controller {
    private readonly UsersService usersService;
    public UsersController(UsersService usersService) {
      this.usersService = usersService;
    }

    public async Task<IActionResult> Index(string email) {
      ResponseModel<IEnumerable<UserDTO>> response = await usersService.GetItems(email);
      if (!response.IsSuccess) {
        return View("ServerError", response);
      }
      return View(response.Data);
    }

    public async Task<IActionResult> Details(int? id) {
      if (id == null) {
        return NotFound();
      }
      ResponseModel<UserDTO> response = await usersService.GetItemById((int)id);
      if (!response.IsSuccess) {
        return View("ServerError", response);
      }
      return View(response.Data);
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Surname,Age,Email,RegistationDate")] UserDTO user) {
      if (ModelState.IsValid) {
        ResponseModel<UserDTO> response = await usersService.CreateItem(user);
        if (response.IsSuccess) {
          ViewBag.Message = "добавлен";
          return View("Success");
        }
        return View("ErrorServer", response);
      }
      return View(user);
    }

    public async Task<IActionResult> Edit(int? id) {
      if (id == null) {
        return NotFound();
      }
      ResponseModel<UserDTO> response = await usersService.GetItemById((int)id);
      if (response.IsSuccess) {
        return View(response.Data);
      }
      return View("ServerError", response);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Age,Email,RegistationDate")] UserDTO user) {
      if (id != user.Id) {
        return NotFound();
      }
      if (ModelState.IsValid) {
        ResponseModel<UserDTO> response = await usersService.UpdateItem(user);
        if (response.IsSuccess) {
          ViewBag.Message = "изменен";
          return View("Success");
        }
        return View("ServerError", response);
      }
      return View(user);
    }

    public async Task<IActionResult> Delete(int? id) {
      if (id == null) {
        return NotFound();
      }
      ResponseModel<UserDTO> response = await usersService.GetItemById((int)id);
      if (!response.IsSuccess) {
        return View(response.Data);
      }
      return View("ServerError", response);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      ResponseModel<UserDTO> response = await usersService.DeleteItem(id);
      if (response.IsSuccess) {
        ViewBag.Message = "удален";
        return View("Success");
      }
      return View("ServerError", response);
    }
  }
}
