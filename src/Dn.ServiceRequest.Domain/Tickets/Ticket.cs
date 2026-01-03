using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dn.ServiceRequest.Tickets
{
    
    public class Ticket : AuditedAggregateRoot<Guid>
    {
        public string Object { get; set; }
        public string Description { get; set; }
        public string Numero { get; set; }
        public Guid Type_id { get; set; }
        public string Json_form { get; set; }
        public Status Status { get; set; }
        public Guid  AssignedTo { get; set; }
        public DateTime ClosureDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PendingDate { get; set; }
        public DateTime EstimateDate { get; set; }





    }
}