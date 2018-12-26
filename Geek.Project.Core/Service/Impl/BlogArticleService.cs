using AutoMapper;
using Geek.Project.Core.Repository.Interface;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Infrastructure.Services;
using Geek.Project.Infrastructure.UnitOfWork;

namespace Geek.Project.Core.Service.Impl
{
    public class BlogArticleService : IBlogArticleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IBlogArticleRepository _blogArticleRepository;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public BlogArticleService(IUnitOfWork uow, IMapper mapper, IBlogArticleRepository blogArticleRepository, IPropertyMappingContainer propertyMappingContainer)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._blogArticleRepository = blogArticleRepository;
            this._propertyMappingContainer = propertyMappingContainer;
        }
    }
}
