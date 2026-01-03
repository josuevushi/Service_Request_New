using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.GroupeUsers
{
    public class GroupeUserDto : AuditedEntityDto<Guid>
    {
        public Guid Groupe_id { get; set; }
        public Guid User_id { get; set; }
        public Boolean Is_Receiver { get; set; }

    }
}