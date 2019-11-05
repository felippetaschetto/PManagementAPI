using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManagement.Core.Entities;
using PManagement.DataProvider.EntityConfig.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.DataProvider.EntityConfig
{
    public class CompanyConfig : BaseEntityConfig<Company>
    {
        public CompanyConfig() : base("Company")
        {
            
        }

        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);
            
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasMany(bc => bc.Users)
                .WithOne(c => c.Company)
                .HasForeignKey(bc => bc.CompanyId);
        }
    }
}
