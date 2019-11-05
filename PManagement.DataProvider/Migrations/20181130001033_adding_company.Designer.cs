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
    [Migration("20181130001033_adding_company")]
    partial class adding_company
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PManagement.Core.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<DateTime>("InsertStamp");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdateStamp");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

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

            modelBuilder.Entity("PManagement.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<int>("CompanyId");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<DateTime>("InsertStamp");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("ProfileImg");

                    b.Property<DateTime?>("UpdateStamp");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PManagement.Core.Entities.User", b =>
                {
                    b.HasOne("PManagement.Core.Entities.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
