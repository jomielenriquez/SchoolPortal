using Microsoft.EntityFrameworkCore;
using Portal.Data;
using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class AdminService : BaseService<Admin>, IAdminService
    {
        private readonly IAdminRepository _admin;

        public AdminService(IAdminRepository admin, IBaseRepository<Admin> adminRepository) : base(adminRepository) 
        { 
            _admin = admin; 
        }
        public Admin GetUsingUsernamePassword(string username, string password)
        {
            return _admin.GetUsingUsernamePassword(username, password);
        }

        public bool IsValidUsernamePassword(string username, string password)
        {
            return _admin.IsValidUsernamePassword(username, password);
        }
    }
}
