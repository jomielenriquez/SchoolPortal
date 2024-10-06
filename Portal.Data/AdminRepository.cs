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
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(PortalDBContext portalDBContext) : base(portalDBContext)
        {
        }
        public Admin GetUsingUsernamePassword(string username, string password)
        {
            return _portalDBContext.Admin
                .Include(a => a.Role)
                .Where(a => a.USERNAME == username && a.PASSWORD == ComputeMd5Hash(password)).FirstOrDefault() ?? new Admin();
        }
        public bool IsValidUsernamePassword(string username, string password)
        {
            var account = _portalDBContext.Admin.Where(a => a.USERNAME == username && a.PASSWORD == ComputeMd5Hash(password)).FirstOrDefault();
            return account != null ? true : false;
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
