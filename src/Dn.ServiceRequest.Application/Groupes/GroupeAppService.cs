using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Dn.ServiceRequest.Groupes
{
    public class GroupeAppService :
    CrudAppService<
        Groupe,
        GroupeDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateGroupeDto>,
    IGroupeAppService
    {
        public GroupeAppService(IRepository<Groupe, Guid> repository) : base(repository)
        {
        }
    }
}