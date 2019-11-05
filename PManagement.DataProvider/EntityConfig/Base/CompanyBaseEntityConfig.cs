using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManagement.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.DataProvider.EntityConfig.Base
{
    public class CompanyBaseEntityConfig<T> : IEntityTypeConfiguration<T>
        where T : CompanyBaseEntity
    {
        private readonly string TableName;

        public CompanyBaseEntityConfig(string tableName)
        {
            this.TableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(this.TableName);

            builder.HasOne(p => p.Company)
               .WithMany()
               .HasForeignKey(b => b.CompanyId);
        }
    }
}
