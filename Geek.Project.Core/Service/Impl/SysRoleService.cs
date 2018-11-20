using AutoMapper;
using Geek.Project.Core.Repository.Interface;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Entity;
using Geek.Project.Infrastructure.Services;
using Geek.Project.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Geek.Project.Core.Service.Impl
{
    public class SysRoleService : ISysRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISysRoleRepository _roleRepository;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public SysRoleService(IUnitOfWork uow, IMapper mapper, ISysRoleRepository roleRepository, IPropertyMappingContainer propertyMappingContainer)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._roleRepository = roleRepository;
            this._propertyMappingContainer = propertyMappingContainer;
        }


        public async Task<List<SysRole>> GetRoleListAsync()
        {
            return await _roleRepository.Query().ToListAsync();
        }
    }
}
