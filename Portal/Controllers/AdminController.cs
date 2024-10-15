using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Portal.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Portal.Data;
using Portal.Data.Entities;
using System.Security.Cryptography;
using System.Text;
using Portal.Models;
using Portal.Data.SearchModel;

namespace Portal.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ISystemParameterService _parameterService;
        private readonly IBaseService<Admin> _baseService;

        public AdminController(IAdminService adminService,
            ISystemParameterService parameterService,
            IBaseService<Admin> baseService)
        {
            _adminService = adminService;
            _parameterService = parameterService;
            _baseService = baseService;
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
                    new Claim(ClaimTypes.Role, currentUser.Role.NAME),
                    new Claim("Id", currentUser.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("SiteInfo", "Admin");
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
        [Authorize(Roles = "Admin")]
        public IActionResult ListScreen(PageModel pageModel)
        {
            AppModel<Admin> newsListScreen = new AppModel<Admin>
            {
                PageModel = new PageModel()
                {
                    Page = pageModel != null ? pageModel.Page : 1,
                    PageSize = pageModel != null ? pageModel.PageSize : 10,
                    OrderByProperty = "USERNAME",
                    IsAscending = pageModel != null ? pageModel.IsAscending : false,
                }
            };

            newsListScreen.Data = _baseService.GetAllWithOptions(newsListScreen.PageModel);
            newsListScreen.Count = _baseService.GetCountWithOptions(newsListScreen.PageModel);
            return View(newsListScreen);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult LoadAddModal(Guid id)
        {
            var news = _baseService.GetWithId(id) ?? new Admin();
            return PartialView("Modals/Admin/_Add", news);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Save(Admin admin)
        {
            admin.ROLEID = new Guid("673ED96D-3A62-4387-91D1-081B4591DFD8");
            if (admin.Id == null)
            {
                admin.PASSWORD = ComputeMd5Hash(admin.PASSWORD);
                _baseService.Save(admin);
            }
            else
            {
                var user = _baseService.GetWithId(admin.Id ?? new Guid());
                if(user.PASSWORD != admin.PASSWORD)
                {
                    admin.PASSWORD = ComputeMd5Hash(admin.PASSWORD);
                }
                _baseService.Update(admin);
            }
            return RedirectToAction("ListScreen");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            var news = _baseService.GetWithId(id);
            if (news != null)
            {
                Guid[] newsToDelete = new Guid[] { id };
                _baseService.DeleteWithIds(newsToDelete);
            }
            return RedirectToAction("ListScreen");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Files()
        {
            return View();
        }
        [Authorize]
        public IActionResult MyAccount(Guid Id)
        {
            return View(_adminService.GetWithId(Id));
        }
        [HttpPost]
        [Authorize]
        public IActionResult SaveMyAccount(Admin admin)
        {
            if(admin.Id != null)
            {
                var account = _adminService.GetWithId(admin.Id ?? new Guid());
                account.FIRSTNAME = admin.FIRSTNAME;
                account.LASTNAME = admin.LASTNAME;
                _adminService.Update(account);
            }

            return RedirectToAction("MyAccount", new { Id = admin.Id });
        }
        [HttpPost]
        [Authorize]
        public IActionResult UpdateMyAccountPassword(Guid id, string currentPassword, string newPassword, string confirmNewPassword)
        {
            var account = _adminService.GetWithId(id);
            if(newPassword != confirmNewPassword)
            {
                ViewBag.Error = "The new password does not match";
                return View("MyAccount");
            }
            else if(account.PASSWORD != ComputeMd5Hash(currentPassword))
            {
                ViewBag.Error = "Invalid Current Password";
                return View("MyAccount");
            }
            else
            {
                account.PASSWORD = ComputeMd5Hash(newPassword);
                _adminService.Update(account);
            }

            return RedirectToAction("MyAccount", new { Id = id });
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
        private string ComputeMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
