using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dn.ServiceRequest.Comments
{
    public class CommentAddDto
    {
       [Required]
       public string Text { get; set; }
        public Guid TicketId { get; set; }
    }
}