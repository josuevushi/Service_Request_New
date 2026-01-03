using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dn.ServiceRequest.GroupeUsers
{
    public class CreateUpdateGroupeUserDto
    {
        [Required]
        public Guid Groupe_id { get; set; }
        [Required]
        public Guid User_id { get; set; }

        public Boolean Is_Receiver { get; set; }

    }
}