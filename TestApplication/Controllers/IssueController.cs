using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestApplication.DAL.Entities;
using TestApplication.Models.IssueModels;
using TestApplication.Other;
using TestApplication.Services.IssueServices.Contracts;
using TestApplication.Services.ProjectServices.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace TestApplication.Controllers
{
    public class IssueController : Controller
    {   
        private RoleManager<Role> _roleManager { get; set; }
        private UserManager<User> _userManager { get; set; }
        private IIssueService _IssueService{ get; set; }
        private  IProjectService _projectService { get; set; }
        public IssueController(IIssueService IssueService, IProjectService projectService, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _IssueService = IssueService;
            _projectService = projectService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "manager, director")]
        public async Task<IActionResult> Create(int Id)
        {
            SelectList StatusList = new SelectList(new IssueStatusEnum[] {IssueStatusEnum.Done, IssueStatusEnum.InProgress, IssueStatusEnum.ToDo, });
            ViewBag.Status = StatusList;
            ViewBag.Users = _projectService.GetUserSelectList(Id);
            return View(_IssueService.GetIssueCreateModel(Id));
        }
        [Authorize(Roles = "manager, director")]
        [HttpPost]
        public async Task<IActionResult> Create(IssueCreateModel issueCreateModel)
        {
            await _IssueService.CreateIssue(issueCreateModel, User);
            int Id = issueCreateModel.ProjectId;
            return RedirectToAction("Details", "Project", new {Id = Id});
        
        }
        [Authorize(Roles = "manager, director")]
        public IActionResult Delete(int Id)
        {
            int ProjectId = _IssueService.DeleteIssue(Id);
            return RedirectToAction("Details","Project", new { Id = ProjectId });
        }

        public async Task<IActionResult> Edit(int Id)
        {
            List<SelectListItem> list = Enum.GetValues(typeof(IssueStatusEnum)).Cast<IssueStatusEnum>().Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }).ToList();
            SelectList data = new SelectList(list, "Value", "Text");
            ViewBag.Status = data;
            var Model = await _IssueService.GetIssueEditModel(Id, User);
            ViewBag.Users = _projectService.GetUserSelectList(Model.ProjectId);
            return View(Model);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(IssueEditModel model)
        {
            int ProjectId = _IssueService.IssueEdit(model);
            return RedirectToAction("Details", "Project", new { Id = ProjectId });
        }

        public async Task<IActionResult> Details(int Id)
        {   
            var Model = _IssueService.GetIssueDetailsModel(Id);
            return View(Model);
        }

        public async Task<IActionResult> Index(int Id)
        {
            var IssuesModels = _IssueService.GetAllIssueIndex(Id);
            return View(IssuesModels);
        }
    }
}
