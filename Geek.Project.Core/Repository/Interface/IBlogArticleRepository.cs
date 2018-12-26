using Geek.Project.Entity;
using Geek.Project.Infrastructure.Repository;

namespace Geek.Project.Core.Repository.Interface
{
    public interface IBlogArticleRepository : IBaseRepository<BlogArticle, string>
    {
    }
}
