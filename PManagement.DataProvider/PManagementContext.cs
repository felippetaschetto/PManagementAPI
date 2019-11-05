using Microsoft.EntityFrameworkCore;
using PManagement.Core.Entities;
using PManagement.Core.Infrastructure.UnitOfWork;
using PManagement.DataProvider.EntityConfig;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PManagement.DataProvider
{
    public class PManagementContext : DbContext, IUnitOfWork
    {
        #region DbSet

        public DbSet<TokenInfo> TokensInfo { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        #endregion


        //Add-Migration
        //Remove-Migration
        //Update-Database
        //Script-Migration (from) (to) 'Script-Migration -From 20180904195021_InitialCreate -To XYZ'
        //github.com/mosh-hamedani/vega
        public PManagementContext(DbContextOptions<PManagementContext> options) : base(options)
        {
            //Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyAllConfigurations();
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new TokenInfoConfig());
            modelBuilder.ApplyConfiguration(new CompanyConfig());
        }

        public async Task<bool> Commit()
        {
            return await this.SaveChangesAsync() > 0;
        }

        public bool CommitSync()
        {
            bool ret = this.SaveChanges() > 0;
            return ret;
        }

        public void CleanUp()
        {
            this.CleanUp();
        }
    }
}