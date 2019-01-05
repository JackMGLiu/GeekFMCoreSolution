using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geek.Project.Entity.EntityConfigurations
{
    public class SysMenuConfiguration : IEntityTypeConfiguration<SysMenu>
    {
        public void Configure(EntityTypeBuilder<SysMenu> builder)
        {
            builder.ToTable(nameof(SysMenu));
            builder.HasKey(m => m.Id);
            builder.Property(m => m.ParentId).HasDefaultValue(0);
            builder.Property(m => m.MenuName).IsRequired().HasMaxLength(50);
            builder.Property(m => m.DisplayName).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Icon).HasMaxLength(50);
            builder.Property(m => m.LinkUrl).HasMaxLength(200);
            builder.Property(m => m.SortCode).HasDefaultValue(0);
            builder.Property(m => m.Permission).HasDefaultValue(2000);
            builder.Property(m => m.IsDisplay).HasDefaultValue(false);
            builder.Property(m => m.Status).IsRequired().HasDefaultValue(0);
            builder.Property(m => m.IsDelete).IsRequired().HasDefaultValue(0);
            builder.Property(m => m.CreateTime).IsRequired();
            builder.Property(m => m.Remark).HasMaxLength(200);
        }
    }
}
