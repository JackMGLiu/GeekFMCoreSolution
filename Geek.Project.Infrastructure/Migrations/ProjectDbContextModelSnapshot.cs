﻿// <auto-generated />
using System;
using Geek.Project.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Geek.Project.Infrastructure.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    partial class ProjectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Geek.Project.Entity.BlogArticle", b =>
                {
                    b.Property<string>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("BlogCategory")
                        .HasMaxLength(2000);

                    b.Property<int>("BlogCommentNum")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("BlogContent")
                        .HasMaxLength(2147483647);

                    b.Property<DateTime>("BlogCreateTime");

                    b.Property<string>("BlogRemark")
                        .HasMaxLength(2147483647);

                    b.Property<string>("BlogSubmitter")
                        .HasMaxLength(50);

                    b.Property<string>("BlogTitle")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("BlogTraffic")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime>("BlogUpdateTime");

                    b.HasKey("BlogId");

                    b.ToTable("BlogArticle");
                });

            modelBuilder.Entity("Geek.Project.Entity.SysLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Class");

                    b.Property<string>("Exception");

                    b.Property<string>("Level");

                    b.Property<string>("Message");

                    b.Property<string>("MessageTemplate");

                    b.Property<string>("Properties");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("Url");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Geek.Project.Entity.SysRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("SysRole");
                });

            modelBuilder.Entity("Geek.Project.Entity.SysUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<int?>("Age")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime?>("CreateTime")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(200);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<int?>("RoleId");

                    b.Property<int?>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime?>("UpdateTime");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("SysUser");
                });

            modelBuilder.Entity("Geek.Project.Entity.SysUser", b =>
                {
                    b.HasOne("Geek.Project.Entity.SysRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}
