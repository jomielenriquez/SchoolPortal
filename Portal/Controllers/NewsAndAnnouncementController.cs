using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Data.Entities;
using Portal.Data.SearchModel;
using Portal.Models;
using Portal.Services;
using System.Globalization;

namespace Portal.Controllers
{
    public class NewsAndAnnouncementController : Controller
    {
        private readonly IBaseService<NewsAndAnnouncements> _baseService;
        public NewsAndAnnouncementController(IBaseService<NewsAndAnnouncements> baseService) { 
            _baseService = baseService;
        }
        [Authorize (Roles = "Admin")]
        public IActionResult ListScreen(PageModel pageModel)
        {
            AppModel<NewsAndAnnouncements> newsListScreen = new AppModel<NewsAndAnnouncements>
            {
                PageModel = new PageModel() {
                    Page = pageModel != null ? pageModel.Page : 1,
                    PageSize = pageModel != null ? pageModel.PageSize : 10,
                    OrderByProperty = "CreateDate",
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
            var news = _baseService.GetWithId(id) ?? new NewsAndAnnouncements();
            return PartialView("Modals/News/_Add", news);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Save(NewsAndAnnouncements newsAndAnnouncements)
        {
            if(newsAndAnnouncements.Id == null)
            {
                newsAndAnnouncements.CreateDate = DateTime.UtcNow;
                _baseService.Save(newsAndAnnouncements);
            }
            else
            {
                newsAndAnnouncements.CreateDate = DateTime.UtcNow;
                _baseService.Update(newsAndAnnouncements);
            }
            return RedirectToAction("ListScreen");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            var news = _baseService.GetWithId(id);
            if(news != null)
            {
                Guid[] newsToDelete = new Guid[] { id };
                _baseService.DeleteWithIds(newsToDelete);
            }
            return RedirectToAction("ListScreen");
        }
    }
}
