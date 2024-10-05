using Microsoft.EntityFrameworkCore;
using Portal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Data
{
    public class PortalDBContext : DbContext
    {
        public PortalDBContext(DbContextOptions<PortalDBContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Role> Role { get; set; }

        public DbSet<SystemParameter> SystemParameter { get; set; }
        public DbSet<SystemParameterType> SystemParameterType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .HasKey(a => a.ID);

            modelBuilder.Entity<Role>()
                .HasKey(a => a.ID);

            modelBuilder.Entity<Admin>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Admins)
                .HasForeignKey(u => u.ROLEID);

            modelBuilder.Entity<SystemParameter>()
                .HasOne(u => u.SystemParameterType)
                .WithMany(r => r.SystemParameters)
                .HasForeignKey(u => u.SystemParameterTypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
