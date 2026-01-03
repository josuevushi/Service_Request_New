using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dn.ServiceRequest.Groupes
{
    public class Groupe : AuditedAggregateRoot<Guid>
    {
        public string Nom { get; set; }
    }
}