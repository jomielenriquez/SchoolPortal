using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Portal.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Portal.Data;

namespace Portal.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ISystemParameterService _parameterService;

        public AdminController(IAdminService adminService,
            ISystemParameterService parameterService)
        {
            _adminService = adminService;
            _parameterService = parameterService;
        }
        public IActionResult Login()
        {
            var test = _parameterService.GetBySystemParameterType();
            var test2 = _adminService.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var currentUser = _adminService.GetUsingUsernamePassword(username, password);
            if (!string.IsNullOrEmpty(currentUser.USERNAME))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, currentUser.FIRSTNAME + " " + currentUser.LASTNAME),
                    new Claim(ClaimTypes.Role, currentUser.Role.NAME)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult SiteInfo()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveInformation(IFormCollection form)
        {
            string name = form["SchoolName"];
            foreach(var data in form.Keys)
            {
                try
                {
                    if (Guid.TryParse(data, out Guid result))
                    {
                        _parameterService.UpdateValue(result, form[data]);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return RedirectToAction("SiteInfo", "Admin");
        }
    }
}
