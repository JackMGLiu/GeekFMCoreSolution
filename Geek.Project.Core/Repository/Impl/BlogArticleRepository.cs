using Geek.Project.Core.Repository.Interface;
using Geek.Project.Entity;
using Geek.Project.Infrastructure.Repository;
using Geek.Project.Infrastructure.UnitOfWork;

namespace Geek.Project.Core.Repository.Impl
{
    public class BlogArticleRepository : BaseRepository<BlogArticle, string>, IBlogArticleRepository
    {
        public BlogArticleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
