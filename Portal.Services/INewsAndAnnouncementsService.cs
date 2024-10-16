﻿using Portal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public interface INewsAndAnnouncementsService : IBaseService<NewsAndAnnouncements>
    {
        IEnumerable<NewsAndAnnouncements> GetValidNews();
    }
}
