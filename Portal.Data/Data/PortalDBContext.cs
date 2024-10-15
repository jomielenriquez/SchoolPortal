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
        public DbSet<NewsAndAnnouncements> NewsAndAnnouncements { get; set; }
        public DbSet<FileStorage> FileStorage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Role>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Admin>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Admins)
                .HasForeignKey(u => u.ROLEID);

            modelBuilder.Entity<SystemParameter>()
                .HasOne(u => u.SystemParameterType)
                .WithMany(r => r.SystemParameters)
                .HasForeignKey(u => u.SystemParameterTypeId);

            modelBuilder.Entity<NewsAndAnnouncements>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<NewsAndAnnouncements>()
                .HasOne(u => u.FileStorage)
                .WithMany(r => r.NewsAndAnnouncements)
                .HasForeignKey(u => u.FileId);

            modelBuilder.Entity<FileStorage>()
                .HasKey(a => a.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
