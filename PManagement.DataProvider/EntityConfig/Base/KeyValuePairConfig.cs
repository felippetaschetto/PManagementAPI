using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManagement.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.DataProvider.EntityConfig.Base
{
    public abstract class KeyValuePairConfig<T> : IEntityTypeConfiguration<T> 
        where T : KeyValuePairEntity
    {
        private readonly string TableName;

        public KeyValuePairConfig(string tableName)
        {
            this.TableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(this.TableName);

            builder.Property(e => e.Value)
                .HasMaxLength(512)
                .IsRequired();
        }
    }
}
