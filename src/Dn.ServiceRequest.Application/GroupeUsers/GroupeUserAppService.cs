using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Dn.ServiceRequest.GroupeUsers
{
    public class GroupeUserAppService :
    CrudAppService<
        GroupeUser,
        GroupeUserDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateGroupeUserDto>,
    IGroupeUserAppService
    {
        public GroupeUserAppService(IRepository<GroupeUser, Guid> repository) : base(repository)
        {
        }
    }
}