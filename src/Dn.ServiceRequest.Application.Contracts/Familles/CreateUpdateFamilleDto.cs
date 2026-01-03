using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dn.ServiceRequest.Familles
{
    public class CreateUpdateFamilleDto
    {
        [Required]
        [StringLength(128)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [StringLength(128)]
        public string Prefixe { get; set; } = string.Empty;
    }
}