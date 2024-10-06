using Portal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public interface IAdminService : IBaseService<Admin>
    {
        public Admin GetUsingUsernamePassword(string username, string password);
        bool IsValidUsernamePassword(string username, string password);
    }
}
