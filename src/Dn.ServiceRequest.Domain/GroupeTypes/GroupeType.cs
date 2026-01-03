using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dn.ServiceRequest.GroupeTypes
{
    public class GroupeType : AuditedAggregateRoot<Guid>
    {
        public Guid Groupe_id { get; set; }
        public Guid Type_id { get; set; }
    }
}