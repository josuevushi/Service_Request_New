using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dn.ServiceRequest.GroupeTypes
{
    public class CreateUpdateGroupeTypeDto
    {
        [Required]
        public Guid Groupe_id { get; set; }
        [Required]
        public Guid Type_id { get; set; }
    }
}