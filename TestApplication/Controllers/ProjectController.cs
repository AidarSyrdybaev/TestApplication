using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestApplication.DAL.Entities;
using TestApplication.Models.ProjectModels;
using TestApplication.Other;
using TestApplication.Services.IssueServices.Contracts;
using TestApplication.Services.ProjectServices.Contracts;
using TestApplication.Services.UserService.Contract;

namespace TestApplication.Controllers
{
    public class ProjectController : Controller
    {
        private RoleManager<Role> _roleManager { get; set; }
        private IProjectService _projectService { get; set; }
        private IUserService _userService { get; set; }
        public ProjectController(IProjectService projectService, RoleManager<Role> roleManager, IUserService userService)
        {
            _roleManager = roleManager;
            _userService = userService;
            _projectService = projectService;
        }
 
        [Authorize(Roles = "director, manager")]
        public IActionResult Create()
        {
            var Values = Enum.GetValues(typeof(ProjectPriorityEnum));
            ViewBag.Priority = new SelectList(Enum.GetValues(typeof(ProjectPriorityEnum)));
            return View(new ProjectCreateModel());

        }
        
        [HttpPost]
        [Authorize(Roles = "director, manager")]
        public async Task<IActionResult> Create(ProjectCreateModel projectCreateModel)
        {

            await _projectService.CreateProjectAsync(projectCreateModel, User);
            return RedirectToAction("Index", "Project");
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(_projectService.GetProjectIndexModels());
        }
        [Authorize(Roles = "director, manager")]
        public async Task<IActionResult> Edit(int Id)
        {
            var user = await _userService._userManager.GetUserAsync(User);
            var ManagerCheck = await _userService._userManager.IsInRoleAsync(user, "manager");
            var DirectorCheck = await _userService._userManager.IsInRoleAsync(user, "director");
            var ProjectModel = _projectService.GetProjectEditModel(Id);
            if (ManagerCheck && user.Id == ProjectModel.UserId || DirectorCheck)
            {
                var Values = Enum.GetValues(typeof(ProjectPriorityEnum));
                ViewBag.Priority = new SelectList(Enum.GetValues(typeof(ProjectPriorityEnum)));
                ViewBag.Managers = await _userService.GetManagerSelectList();
                ProjectModel.IsDirector = DirectorCheck;
                return View(ProjectModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "director, manager")]
        public async Task<IActionResult> Edit(ProjectEditModel projectEditModel)
        {
            
            _projectService.EditProject(projectEditModel);
            return RedirectToAction("Details", new { Id = projectEditModel.Id});
            
        }
        [Authorize]
        public async Task<IActionResult> Details(int Id)
        {   
            
            ViewBag.Users = _projectService.GetUserSelectList(Id);
            var ProjectModel = _projectService.GetProjectDetailModel(Id);
            return View(ProjectModel);
        }

        [Authorize(Roles = "director")]
        public async Task<IActionResult> Delete(int Id)
        {
            _projectService.Remove(Id);
            return RedirectToAction("Index");
        }


    }
}
