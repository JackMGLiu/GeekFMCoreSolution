using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geek.Project.Entity.Configurations
{
    public class SysLogConfiguration : IEntityTypeConfiguration<SysLog>
    {
        public void Configure(EntityTypeBuilder<SysLog> builder)
        {
            builder.ToTable("Logs");
            builder.HasKey(m => m.Id);
        }
    }
}
