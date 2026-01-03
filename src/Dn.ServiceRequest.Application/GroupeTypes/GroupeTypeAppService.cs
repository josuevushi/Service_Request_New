using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Dn.ServiceRequest.GroupeTypes
{
    public class GroupeTypeAppService :
    CrudAppService<
        GroupeType,
        GroupeTypeDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateGroupeTypeDto>,
    IGroupeTypeAppService
    {
        public GroupeTypeAppService(IRepository<GroupeType, Guid> repository) : base(repository)
        {
        }
    }
}