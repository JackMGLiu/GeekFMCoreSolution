using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geek.Project.Entity.Configurations
{
    public class SysRoleConfiguration : IEntityTypeConfiguration<SysRole>
    {
        public void Configure(EntityTypeBuilder<SysRole> builder)
        {
            builder.ToTable("SysRole");
            builder.HasKey(m => m.Id);
        }
    }
}
