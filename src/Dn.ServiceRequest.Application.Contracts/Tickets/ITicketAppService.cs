using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Dn.ServiceRequest.Tickets
{
    public interface ITicketAppService :
    ICrudAppService< 
        TicketDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateTicketDto> 
    {
        Task<List<IdentityUserDto>> GetUsersInGroups();
        Task AssignTicketToUser(Guid ticketId, Guid userId);
        Task<List<MesTicketsDto>> GetTicketsAssignes();
        Task<List<MesTicketsDto>> GetTicketsGroupe();
        Task<List<MesTicketsDto>> GlobalSearchAsync(string numero);
        Task CloseTicketAsync(Guid id);
    }
}