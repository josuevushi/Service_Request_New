using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dn.ServiceRequest.Types
{
    public class Type : AuditedAggregateRoot<Guid>
    {
        public string Nom { get; set; }
        public Guid Famille_id { get; set; }
        public string Json_form { get; set; }      
        public string Json_step { get; set; }
        public int Sla { get; set; }
    }
}