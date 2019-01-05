using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geek.Project.Entity.EntityConfigurations
{
    public class BlogArticleConfiguration : IEntityTypeConfiguration<BlogArticle>
    {
        public void Configure(EntityTypeBuilder<BlogArticle> builder)
        {
            builder.ToTable(nameof(BlogArticle));
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).IsRequired().HasMaxLength(50);
            builder.Property(m => m.BlogSubmitter).HasMaxLength(50);
            builder.Property(m => m.BlogTitle).IsRequired().HasMaxLength(200);
            builder.Property(m => m.BlogCategory).HasMaxLength(2000);
            builder.Property(m => m.BlogContent).HasMaxLength(int.MaxValue);
            builder.Property(m => m.BlogTraffic).HasDefaultValue(0);
            builder.Property(m => m.BlogCommentNum).HasDefaultValue(0);
            builder.Property(m => m.BlogCreateTime).IsRequired();
            builder.Property(m => m.BlogRemark).HasMaxLength(int.MaxValue);
        }
    }
}
