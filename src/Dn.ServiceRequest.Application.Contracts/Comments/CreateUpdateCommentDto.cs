using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dn.ServiceRequest.Comments
{
    public class CreateUpdateCommentDto
    {
       public string Text { get; set; }
       
        public Guid Ticket_Id { get; set; }
    }
}