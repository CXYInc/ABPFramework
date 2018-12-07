﻿// <auto-generated />
using System;
using CXY.CJS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CXY.CJS.Migrations
{
    [DbContext(typeof(CJSDbContext))]
    [Migration("20181207053631_20181207133604")]
    partial class _20181207133604
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CXY.CJS.Model.Test", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(65);

                    b.Property<string>("Name")
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.ToTable("Tests","CXY");
                });

            modelBuilder.Entity("CXY.CJS.Model.WebSite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConnectionString")
                        .HasMaxLength(65);

                    b.Property<DateTime>("CreationTime")
                        .HasMaxLength(65);

                    b.Property<string>("CreatorUserId")
                        .HasMaxLength(65);

                    b.Property<string>("DeleterUserId")
                        .HasMaxLength(65);

                    b.Property<DateTime?>("DeletionTime")
                        .HasMaxLength(65);

                    b.Property<bool>("IsDeleted")
                        .HasMaxLength(65);

                    b.Property<DateTime?>("LastModificationTime")
                        .HasMaxLength(65);

                    b.Property<string>("LastModifierUserId")
                        .HasMaxLength(65);

                    b.Property<string>("WebSiteId")
                        .HasMaxLength(65);

                    b.Property<string>("WebSiteName")
                        .HasMaxLength(65);

                    b.HasKey("Id");

                    b.ToTable("WebSites","CXY");
                });
#pragma warning restore 612, 618
        }
    }
}
