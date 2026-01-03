using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.Comments
{
    public class CommentDto : AuditedEntityDto<Guid>
    {
        public string Text { get; set; }
        
        public Guid Ticket_Id { get; set; }
    }
}