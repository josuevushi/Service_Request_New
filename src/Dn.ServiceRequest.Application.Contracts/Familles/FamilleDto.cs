using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.Familles
{
    public class FamilleDto : AuditedEntityDto<Guid>
    {
        public string Nom { get; set; }
        public string Prefixe { get; set; } 
    }
}