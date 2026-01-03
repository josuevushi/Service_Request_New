using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.GroupeTypes
{
    public class GroupeTypeDto : AuditedEntityDto<Guid>
    {
        public Guid Groupe_id { get; set; }
        public Guid Type_id { get; set; }

    }
}