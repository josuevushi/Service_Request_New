using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dn.ServiceRequest.PieceJointes
{
    public class CreateUpdatePieceJointeDto
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Path { get; set; }
        
        public Guid Ticket_id { get; set; }
    }
}