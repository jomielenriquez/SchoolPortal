using Microsoft.EntityFrameworkCore;
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
    public class NewsAndAnnouncementsRepository : BaseRepository<NewsAndAnnouncements>, INewsAndAnnouncementsRepository
    {
        public NewsAndAnnouncementsRepository(PortalDBContext portalDBContext) : base(portalDBContext)
        {
        }

        public IEnumerable<NewsAndAnnouncements> GetValidNews()
        {
            return _portalDBContext.NewsAndAnnouncements
                .Include(x => x.FileStorage)
                .Where(x => x.IsDeleted == false && (x.ExpirationDate == null || x.ExpirationDate >= DateTime.Now))
                .OrderBy(x => x.Order);
        }
    }
}
