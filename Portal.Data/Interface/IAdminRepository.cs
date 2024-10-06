using Portal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Interface
{
    public interface IAdminRepository
    {
        public Admin GetUsingUsernamePassword(string username, string password);
        bool IsValidUsernamePassword(string username, string password);

    }
}
