using AutoMapper;
using Geek.Project.Core.Repository.Interface;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel.SysLog;
using Geek.Project.Entity;
using Geek.Project.Infrastructure.Extensions;
using Geek.Project.Infrastructure.QueryModel;
using Geek.Project.Infrastructure.Services;
using Geek.Project.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geek.Project.Core.Service.Impl
{
    public class SysLogService : ISysLogService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISysLogRepository _logRepository;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public SysLogService(IUnitOfWork uow, IMapper mapper, ISysLogRepository logRepository, IPropertyMappingContainer propertyMappingContainer)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._logRepository = logRepository;
            this._propertyMappingContainer = propertyMappingContainer;
        }

        public async Task<PagedList<SysLog>> GetAllLogsAsync(LogParameters parameters)
        {
            var query = _logRepository.Query();
            if (!string.IsNullOrEmpty(parameters.Level))
            {
                var level = parameters.Level.ToLowerInvariant();
                query = query.Where(x => x.Level.ToLowerInvariant().Contains(level));
            }
            if (!string.IsNullOrEmpty(parameters.TimeStamp))
            {
                var time = parameters.TimeStamp.Trim().Split('-');
                var start = DateTime.Parse(time[0] + '-' + time[1] + '-' + time[2]);
                var end = DateTime.Parse(time[3] + '-' + time[4] + '-' + time[5]);
                query = query.Where(x => x.TimeStamp >= start && x.TimeStamp <= end);
            }

            query = query.ApplySort(parameters.OrderBy, _propertyMappingContainer.Resolve<SysLogViewModel, SysLog>()); //排序

            var count = await query.CountAsync();
            var data = await query
                .Skip((parameters.PageIndex - 1) * parameters.PageSize)
                .Take(parameters.PageIndex * parameters.PageSize)
                .ToListAsync();
            return new PagedList<SysLog>(parameters.PageIndex, parameters.PageSize, count, data);
        }
    }
}
