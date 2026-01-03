using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dn.ServiceRequest.Types
{
    public class CreateUpdateTypeDto
    {
      
        public Guid Famille_id { get; set; }
        public string Nom { get; set; }
        public string Json_form { get; set; }
        public string Json_step { get; set; }
        
        [Required]
        public int Sla { get; set; }
        public DateTime ClosureDate { get; set; }

    }
}