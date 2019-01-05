using Geek.Project.Entity.Base;

namespace Geek.Project.Infrastructure.Services
{
    public interface IPropertyMappingContainer<TKey>
    {
        void Register<T>() where T : IPropertyMapping, new();
        IPropertyMapping Resolve<TSource, TDestination>() where TDestination : IEntity<TKey>;
        bool ValidateMappingExistsFor<TSource, TDestination>(string fields) where TDestination : IEntity<TKey>;
    }
}
