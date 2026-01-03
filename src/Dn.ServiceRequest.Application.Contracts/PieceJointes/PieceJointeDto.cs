using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.PieceJointes
{
    public class PieceJointeDto : AuditedEntityDto<Guid>
    {
        public string Nom { get; set; }
        public string Path { get; set; }
        public Guid Ticket_id { get; set; }
    }
}