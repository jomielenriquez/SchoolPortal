using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class AdminService : IAdminService
    {
        private readonly IBaseRepository<Admin> _admin;

        public AdminService(IBaseRepository<Admin> admin) { _admin = admin; }
        public int DeleteWithIds(Guid[] id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Admin> GetAll()
        {
            return _admin.GetAll();
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
