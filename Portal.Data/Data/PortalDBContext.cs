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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .HasKey(a => a.ID);

            modelBuilder.Entity<Role>()
                .HasKey(a => a.ID);

            modelBuilder.Entity<Admin>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Admin)
                .HasForeignKey(u => u.ROLEID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
