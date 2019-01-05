using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geek.Project.Entity.Configurations
{
    public class SysRoleConfiguration : IEntityTypeConfiguration<SysRole>
    {
        public void Configure(EntityTypeBuilder<SysRole> builder)
        {
            builder.ToTable(nameof(SysRole));
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).IsRequired().HasMaxLength(50);
            builder.Property(m => m.RoleName).IsRequired().HasMaxLength(50);
            builder.Property(m => m.IsSuperManager).IsRequired().HasDefaultValue(false);
            builder.Property(m => m.IsDefault).IsRequired().HasDefaultValue(false);
            builder.Property(m => m.Status).IsRequired().HasDefaultValue(0);
            builder.Property(m => m.IsDelete).IsRequired().HasDefaultValue(0);
            builder.Property(m => m.CreateTime).IsRequired();
            builder.Property(m => m.Remark).HasMaxLength(200);
        }
    }
}
