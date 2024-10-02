using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Interface
{
    public interface IAdminRepository<Admin>
    {
        IEnumerable<Admin> GetAll();
        //IEnumerable<T> GetAllWithOptions(PageModel pageModel);
        //int GetCountWithOptions(PageModel pageModel);
        int DeleteWithIds(Guid[] id);
        int Save(Admin data);
        Admin GetWithId(Guid id);
        int Update(Admin data);
        Admin GetUsingUsernamePassword(string username, string password);
        bool IsValidUsernamePassword(string username, string password);

    }
}
