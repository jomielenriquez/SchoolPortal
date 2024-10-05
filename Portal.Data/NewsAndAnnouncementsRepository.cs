using Portal.Data.Data;
using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data
{
    public class NewsAndAnnouncementsRepository : IBaseRepository<NewsAndAnnouncements>, INewsAndAnnouncementsRepository
    {
        private readonly PortalDBContext _dbContext;
        public NewsAndAnnouncementsRepository(PortalDBContext dbContext)
        {
            _dbContext = dbContext;
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
            return _dbContext.NewsAndAnnouncements
                .Where(x => x.IsDeleted == false && (x.ExpirationDate == null || x.ExpirationDate >= DateTime.Now))
                .OrderBy(x => x.Order);
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
