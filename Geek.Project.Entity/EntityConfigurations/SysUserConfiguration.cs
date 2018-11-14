using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geek.Project.Entity.Configurations
{
    public class SysUserConfiguration : IEntityTypeConfiguration<SysUser>
    {
        public void Configure(EntityTypeBuilder<SysUser> builder)
        {
            builder.ToTable("SysUser");
            builder.HasKey(m => m.Id);
            builder.HasOne(u => u.Role).WithMany().HasForeignKey(u => u.RoleId);
        }
    }
}
