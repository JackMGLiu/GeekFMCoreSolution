using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geek.Project.Entity.EntityConfigurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable(nameof(RolePermission));
            builder.HasKey(m => m.Id);
            builder.Property(m => m.RoleId).IsRequired().HasMaxLength(50);
            builder.Property(m => m.MenuId).IsRequired();
            builder.Property(m => m.Permission).HasMaxLength(2000);
        }
    }
}
