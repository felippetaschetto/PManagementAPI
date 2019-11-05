using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManagement.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.DataProvider.EntityConfig.Base
{
    public abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> 
        where T : BaseEntity
    {
        private readonly string TableName;

        public BaseEntityConfig(string tableName)
        {
            this.TableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(this.TableName);
            builder.HasKey(p => p.Id);
        }
    }
}
