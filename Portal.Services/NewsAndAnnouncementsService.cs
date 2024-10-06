using Portal.Data;
using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class NewsAndAnnouncementsService : BaseService<NewsAndAnnouncements>, INewsAndAnnouncementsService
    {
        private readonly INewsAndAnnouncementsRepository _newsAndAnnouncementsRepository;
        private readonly IBaseRepository<NewsAndAnnouncements> _newsRepository;
        public NewsAndAnnouncementsService(IBaseRepository<NewsAndAnnouncements> newsRepository,
            INewsAndAnnouncementsRepository newsAndAnnouncementsRepository) : base(newsRepository)
        {
            _newsAndAnnouncementsRepository = newsAndAnnouncementsRepository;
            _newsRepository = newsRepository;
        }

        public IEnumerable<NewsAndAnnouncements> GetValidNews()
        {
            return _newsAndAnnouncementsRepository.GetValidNews();
        }
    }
}
