using Portal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Interface
{
    public interface INewsAndAnnouncementsRepository
    {
        IEnumerable<NewsAndAnnouncements> GetValidNews();
    }
}
