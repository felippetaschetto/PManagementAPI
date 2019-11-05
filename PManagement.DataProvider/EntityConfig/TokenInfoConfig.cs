using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.DataProvider.EntityConfig
{
    public class TokenInfoConfig : IEntityTypeConfiguration<TokenInfo>
    {
        public void Configure(EntityTypeBuilder<TokenInfo> builder)
        {
            builder.ToTable("TokenInfo");

            builder.Property(e => e.Token)
                .IsRequired();

            builder.Property(e => e.RenewKey)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.ExpireDate)
                .IsRequired();
        }
    }
}
