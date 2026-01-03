using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dn.ServiceRequest.PieceJointes;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.Tickets
{
    public class NewTicketDto

    {        
    public string Objet { get; set; }
    public string Description { get; set; }
    public string JsonFrom { get; set; }
    public string IdenType { get; set; }
    public List<string>? Fichiers { get; set; }

    }
}