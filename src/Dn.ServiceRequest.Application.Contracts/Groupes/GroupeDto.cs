using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.Groupes
{
    public class GroupeDto : AuditedEntityDto<Guid>
    {
        public string Nom { get; set; }
    }
}