using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dn.ServiceRequest.PieceJointes
{
    public interface IPieceJointeAppService :
    ICrudAppService< 
        PieceJointeDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdatePieceJointeDto> 
    {
        Task<List<PieceJointeDto>> GetListByTicketId(Guid ticketId);
        Task<PieceJointeDto> GetAddPieceJointe(string ticketId, string nom, string path);
    }
}