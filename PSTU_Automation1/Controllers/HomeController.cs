using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PSTU_Automation1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PSTU_Automation1.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser>signInManager
            )
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {

                var user = await _userManager.GetUserAsync(User);

                if (await _userManager.IsInRoleAsync(user, "Student"))
                {
                    return View();
                }
                else if (await _userManager.IsInRoleAsync(user, "Teacher"))
                {
                    // return RedirectToAction("TeacherProfile", "Home");
                    return View();
                }
                else if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    // return RedirectToAction("Admin", "Home");
                    return View();
                }
            }
            return View();
        }
        [Authorize(Roles = "Student")]
        public IActionResult StudentProfile()
        {
            return View();
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult TeacherProfile()
        {
          

                return View();
              
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult About_Us()
        {
            return View();
        }
        public IActionResult TotalStudent()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult U_Ad_View()  
        {
            return View();
        }

    public IActionResult Post_AD_View()  
        {
            return View();
}
     public IActionResult history()
        {
            return View();
        }
        public IActionResult MessageVc()
        {
            return View();
        }
        
       

        public IActionResult Faculties()
        {
            return View();
        }
        public IActionResult Addmission()
        {
            return View();
        }
        public IActionResult Notice()
        {
            return View();
        }
       
        public IActionResult Enroll_Course()
        {
            return View();
        }
        public IActionResult Create()
        {
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
