using AutoMapper;
using Geek.Project.Core.ViewModel.SysRole;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Entity;

namespace Geek.Project.Portal.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SysUser, UserViewModel>();
            CreateMap<UserViewModel, SysUser>();


            CreateMap<SysRole, RoleViewModel>();
            CreateMap<RoleViewModel, SysRole>();

            //CreateMap<PostAddViewModel, Post>();
            //CreateMap<PostUpdateResource, Post>();
        }
    }
}
