using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class NewsAndAnnouncementsService : IBaseService<NewsAndAnnouncements>, INewsAndAnnouncementsService
    {
        private readonly IBaseRepository<NewsAndAnnouncements> _baseRepository;
        private readonly INewsAndAnnouncementsRepository _newsAndAnnouncementsRepository;
        public NewsAndAnnouncementsService(IBaseRepository<NewsAndAnnouncements> baseRepository,
            INewsAndAnnouncementsRepository newsAndAnnouncementsRepository)
        {
            _baseRepository = baseRepository;
            _newsAndAnnouncementsRepository = newsAndAnnouncementsRepository;
        }
        public int DeleteWithIds(Guid[] id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewsAndAnnouncements> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewsAndAnnouncements> GetValidNews()
        {
            return _newsAndAnnouncementsRepository.GetValidNews();
        }

        public NewsAndAnnouncements GetWithId(Guid id)
        {
            throw new NotImplementedException();
        }

        public NewsAndAnnouncements Save(NewsAndAnnouncements data)
        {
            throw new NotImplementedException();
        }

        public NewsAndAnnouncements Update(NewsAndAnnouncements data)
        {
            throw new NotImplementedException();
        }
    }
}
