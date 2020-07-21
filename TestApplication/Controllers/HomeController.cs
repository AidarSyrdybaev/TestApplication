using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApplication.DAL.Entities;
using TestApplication.Models;
using TestApplication.Services.ProjectServices.Contracts;

namespace TestApplication.Controllers
{
    public class HomeController : Controller
    {
        private IProjectService _projectService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IProjectService projectService, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _logger = logger;
            projectService = _projectService;
            RoleInitializer.userManager = userManager;
            RoleInitializer.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            await RoleInitializer.InitializeAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
