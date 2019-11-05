﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PManagement.DataProvider;

namespace PManagement.DataProvider.Migrations
{
    [DbContext(typeof(PManagementContext))]
    [Migration("20181130000907_creating_user_and_company")]
    partial class creating_user_and_company
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PManagement.Core.Entities.TokenInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExpireDate");

                    b.Property<DateTime>("InsertStamp");

                    b.Property<string>("RenewKey");

                    b.Property<string>("Token");

                    b.Property<DateTime?>("UpdateStamp");

                    b.HasKey("Id");

                    b.ToTable("TokenInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
