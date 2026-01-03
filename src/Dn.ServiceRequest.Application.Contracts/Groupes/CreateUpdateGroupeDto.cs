using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dn.ServiceRequest.Groupes
{
    public class CreateUpdateGroupeDto
    {
        [Required]
        [StringLength(128)]
        public string Nom { get; set; } = string.Empty;

    }
}