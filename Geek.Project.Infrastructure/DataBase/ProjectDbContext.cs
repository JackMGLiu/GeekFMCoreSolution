using Geek.Project.Entity;
using Geek.Project.Entity.Configurations;
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
        public DbSet<SysLog> sysLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SysUserConfiguration());
            modelBuilder.ApplyConfiguration(new SysRoleConfiguration());
            modelBuilder.ApplyConfiguration(new SysLogConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
