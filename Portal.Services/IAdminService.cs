using Portal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public interface IAdminService
    {
        IEnumerable<Admin> GetAll();
        //IEnumerable<T> GetAllWithOptions(PageModel pageModel);
        //int GetCountWithOptions(PageModel pageModel);
        int DeleteWithIds(Guid[] id);
        int Save(Admin data);
        Admin GetWithId(Guid id);
        int Update(Admin data);
    }
}
