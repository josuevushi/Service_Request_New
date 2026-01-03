using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Dn.ServiceRequest.Familles
{
    public class FamilleAppService :
    CrudAppService<
        Famille, 
        FamilleDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateFamilleDto>, 
    IFamilleAppService 
    {
        public FamilleAppService(IRepository<Famille, Guid> repository) : base(repository)
        {
        }
    }
}