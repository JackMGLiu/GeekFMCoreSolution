using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geek.Project.Infrastructure.Migrations
{
    public partial class _20190104001Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogArticle",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    BlogSubmitter = table.Column<string>(maxLength: 50, nullable: true),
                    BlogTitle = table.Column<string>(maxLength: 200, nullable: false),
                    BlogCategory = table.Column<string>(maxLength: 2000, nullable: true),
                    BlogContent = table.Column<string>(maxLength: 2147483647, nullable: true),
                    BlogTraffic = table.Column<int>(nullable: false, defaultValue: 0),
                    BlogCommentNum = table.Column<int>(nullable: false, defaultValue: 0),
                    BlogUpdateTime = table.Column<DateTime>(nullable: false),
                    BlogCreateTime = table.Column<DateTime>(nullable: false),
                    BlogRemark = table.Column<string>(maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogArticle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(maxLength: 50, nullable: false),
                    MenuId = table.Column<int>(nullable: false),
                    Permission = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysMenu",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: true, defaultValue: 0),
                    MenuName = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    LinkUrl = table.Column<string>(maxLength: 200, nullable: true),
                    SortCode = table.Column<int>(nullable: true, defaultValue: 0),
                    Permission = table.Column<string>(nullable: true, defaultValue: "2000"),
                    IsDisplay = table.Column<bool>(nullable: true, defaultValue: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    IsDelete = table.Column<int>(nullable: false, defaultValue: 0),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    IsSuperManager = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDefault = table.Column<bool>(nullable: false, defaultValue: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    IsDelete = table.Column<int>(nullable: false, defaultValue: 0),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    RoleId = table.Column<string>(maxLength: 50, nullable: true),
                    RealName = table.Column<string>(maxLength: 50, nullable: false),
                    NickName = table.Column<string>(maxLength: 50, nullable: true),
                    PhotoPath = table.Column<string>(maxLength: 200, nullable: true),
                    LoginCount = table.Column<int>(nullable: true, defaultValue: 0),
                    LastLoginIp = table.Column<string>(maxLength: 50, nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    Age = table.Column<int>(nullable: true, defaultValue: 0),
                    Mobile = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    IsDelete = table.Column<int>(nullable: false, defaultValue: 0),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogArticle");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "SysMenu");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "SysUser");
        }
    }
}
