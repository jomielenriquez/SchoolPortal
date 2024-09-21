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
    public class AdminRepository : IBaseRepository<Admin>
    {
        private readonly PortalDBContext _dbContext;

        public AdminRepository(PortalDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int DeleteWithIds(Guid[] id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Admin> GetAll()
        {
            return _dbContext.Admin.ToList();
        }

        public Admin GetWithId(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Save(Admin data)
        {
            throw new NotImplementedException();
        }

        public int Update(Admin data)
        {
            throw new NotImplementedException();
        }
    }
}
