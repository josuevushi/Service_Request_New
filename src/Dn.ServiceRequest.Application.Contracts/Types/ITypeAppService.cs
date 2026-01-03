using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dn.ServiceRequest.Types
{
    public interface ITypeAppService :
    ICrudAppService< 
        TypeDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateTypeDto> 
    {
        
    }
}