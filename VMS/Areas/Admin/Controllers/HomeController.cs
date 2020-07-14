using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VMS.Models.ViewModels;


namespace VMS.Areas.Admin.Controllers
{
    [Area("Admin")] //Only Admin functions in this application.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        //this is hard-coded because we do not have a shared database to build on. 
        //Don't change this or you are not getting in - Would never do this in the real world
        [HttpPost]
        public IActionResult Index(LoginViewModel login)
        {
            var userName = login.User;
            var password = login.Password;
            
            if (( userName == "admin") && (password ==  "password"))
            {
                return View("Admin");
            }
            else
            {
                
                return View();
            }
        }


    }
}