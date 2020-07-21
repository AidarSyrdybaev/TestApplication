using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApplication.DAL.Entities;
using TestApplication.Models.UserModels;
using TestApplication.Services.UserService.Contract;

namespace TestApplication.Controllers
{
    public class UserController : Controller
    {

        private IUserService _userService { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(int Id)
        {
            var Users = _userService.GetUsersInProject(Id);
            ViewBag.Users = _userService.GetUserSelectList();
            return View(Users);
        }
        [HttpPost]
        [Authorize(Roles = "manager, director")]
        public async Task<IActionResult> Add(int SelectedUserId, int ProjectId)
        {
            _userService.AddUserInProject(SelectedUserId, ProjectId);
            return RedirectToAction("Index", new {Id = ProjectId});
        }
        [Authorize(Roles = "manager, director")]
        public async Task<IActionResult> Delete(int SelectedUserId, int ProjectId)
        {
            _userService.RemoveUserInProject(SelectedUserId, ProjectId);
            return RedirectToAction("Index", new {Id = ProjectId});
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var model = _userService.GetUserEditModel(Id);
            return View(model);

        }
        [HttpPost]
        [Authorize(Roles = "director")]
        public async Task<IActionResult> Edit(UserEditModel userEditModel)
        {
            await _userService.UserEdit(userEditModel);
            return RedirectToAction("Index", "Project");
        }
        [Authorize]
        public async Task<IActionResult> Details(int Id)
        {
            _userService.Remove(Id);
            return View(_userService.GetUserDetailModel(Id));
        }
        [Authorize(Roles = "director")]
        public async Task<IActionResult> UserDelete(int Id)
        {
            return RedirectToAction("Index", "Project");
        }
    }
}
