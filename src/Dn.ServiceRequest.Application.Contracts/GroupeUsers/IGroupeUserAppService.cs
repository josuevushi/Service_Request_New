using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dn.ServiceRequest.GroupeUsers
{
    public interface IGroupeUserAppService :
    ICrudAppService< 
        GroupeUserDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateGroupeUserDto> 
    {
        
    }
}