using Geek.Project.Entity;
using Geek.Project.Entity.Configurations;
using Geek.Project.Entity.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Geek.Project.Infrastructure.DataBase
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }

        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SysUserConfiguration());
            modelBuilder.ApplyConfiguration(new SysRoleConfiguration());
            modelBuilder.ApplyConfiguration(new BlogArticleConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
