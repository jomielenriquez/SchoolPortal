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
    public class FileStorageRepository : BaseRepository<FileStorage>
    {
        public FileStorageRepository(PortalDBContext portalDBContext) : base(portalDBContext)
        {
        }
    }
}
