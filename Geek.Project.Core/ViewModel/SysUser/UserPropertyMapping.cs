using Geek.Project.Infrastructure.Services;
using System;
using System.Collections.Generic;

namespace Geek.Project.Core.ViewModel.SysUser
{
    public class UserPropertyMapping : PropertyMapping<UserViewModel, Entity.SysUser>
    {
        public UserPropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>
                (StringComparer.OrdinalIgnoreCase)
            {
                //[nameof(PostViewModel.Title)] = new List<MappedProperty>
                //    {
                //        new MappedProperty{ Name = nameof(Post.Title), Revert = false}
                //    },
                //[nameof(PostViewModel.Body)] = new List<MappedProperty>
                //    {
                //        new MappedProperty{ Name = nameof(Post.Body), Revert = false}
                //    },
                //[nameof(PostViewModel.Author)] = new List<MappedProperty>
                //    {
                //        new MappedProperty{ Name = nameof(Post.Author), Revert = false}
                //    }

                [nameof(UserViewModel.Age)] = new List<MappedProperty>
                {
                    new MappedProperty{ Name = nameof(Entity.SysUser.Age), Revert = false}
                }
            })
        {

        }
    }
}
