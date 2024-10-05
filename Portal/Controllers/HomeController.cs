using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Services;
using System.Diagnostics;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminService _adminService;
        private readonly INewsAndAnnouncementsService _newsAnnouncementsService;

        public HomeController(ILogger<HomeController> logger,
            IAdminService adminService,
            INewsAndAnnouncementsService newsAnnouncementsService)
        {
            _logger = logger;
            _adminService = adminService;
            _newsAnnouncementsService = newsAnnouncementsService;
        }

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Programs()
        {
            return View();
        }
        public IActionResult UnderConstruction()
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
