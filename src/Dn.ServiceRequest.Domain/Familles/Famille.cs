using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dn.ServiceRequest.Familles
{
    public class Famille : AuditedAggregateRoot<Guid>
    {
    public string Nom { get; set; }
    public string Prefixe { get; set; }

    }
}