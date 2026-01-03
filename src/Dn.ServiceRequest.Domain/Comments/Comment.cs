using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dn.ServiceRequest.Comments
{
    public class Comment : AuditedAggregateRoot<Guid>
    {
        public string Text { get; set; }
        public Guid Ticket_Id { get; set; }
    }
}