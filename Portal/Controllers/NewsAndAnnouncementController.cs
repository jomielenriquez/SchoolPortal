using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{
    public class NewsAndAnnouncementController : Controller
    {
        public IActionResult ListScreen()
        {
            return View();
        }
    }
}
