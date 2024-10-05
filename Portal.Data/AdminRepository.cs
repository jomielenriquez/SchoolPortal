using Microsoft.EntityFrameworkCore;
using Portal.Data.Data;
using Portal.Data.Entities;
using Portal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data
{
    public class AdminRepository : IAdminRepository<Admin>
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

        public Admin GetUsingUsernamePassword(string username, string password)
        {
            return _dbContext.Admin
                .Include(a => a.Role)
                .Where(a => a.USERNAME == username && a.PASSWORD == ComputeMd5Hash(password)).FirstOrDefault() ?? new Admin();
        }

        public Admin GetWithId(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool IsValidUsernamePassword(string username, string password)
        {
            var account = _dbContext.Admin.Where(a => a.USERNAME == username && a.PASSWORD == ComputeMd5Hash(password)).FirstOrDefault();
            return account != null ? true : false;
        }

        public int Save(Admin data)
        {
            throw new NotImplementedException();
        }

        public int Update(Admin data)
        {
            throw new NotImplementedException();
        }
        private string ComputeMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
