using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dn.ServiceRequest.PieceJointes
{
    public class PieceJointe : AuditedAggregateRoot<Guid>
    {
        public string Nom { get; set; }
        public string Path { get; set; }
        public Guid Ticket_id { get; set; }
    }
}