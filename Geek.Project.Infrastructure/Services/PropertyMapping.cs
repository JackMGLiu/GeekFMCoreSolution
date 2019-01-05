using Geek.Project.Entity.Base;
using System.Collections.Generic;

namespace Geek.Project.Infrastructure.Services
{
    public abstract class PropertyMapping<TKey, TSource, TDestination> : IPropertyMapping
        where TDestination : IEntity<TKey>
    {
        public Dictionary<string, List<MappedProperty>> MappingDictionary { get; }

        protected PropertyMapping(Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
            MappingDictionary[nameof(IEntity<TKey>.Id)] = new List<MappedProperty>
            {
                new MappedProperty { Name = nameof(IEntity<TKey>.Id), Revert = false}
            };
        }
    }
}
