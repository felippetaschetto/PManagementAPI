using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManagement.Core.Entities;
using PManagement.DataProvider.EntityConfig.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.DataProvider.EntityConfig
{
    public class UserConfig : CompanyBaseEntityConfig<User>
    {
        public UserConfig() : base("User")
        {
            //HasRequired(c => c.Company)
            //    .WithMany(u => u.Users)
            //    .HasForeignKey(c => c.CompanyId);
        }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.OwnsOne(o => o.Address);

        }
    }
}
