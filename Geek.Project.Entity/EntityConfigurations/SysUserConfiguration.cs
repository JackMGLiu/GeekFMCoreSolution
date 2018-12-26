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
            builder.Property(m => m.UserName).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Password).IsRequired().HasMaxLength(200);
            builder.Property(m => m.RealName).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Age).HasDefaultValue(0);
            builder.Property(m => m.Email).HasMaxLength(200);
            builder.Property(m => m.Address).HasMaxLength(200);
            builder.Property(m => m.Status).IsRequired().HasDefaultValue(0);
            builder.Property(m => m.CreateTime).IsRequired();
            builder.Property(m => m.Remark).HasMaxLength(200);
            builder.HasOne(u => u.Role).WithMany().HasForeignKey(u => u.RoleId);
        }
    }
}
