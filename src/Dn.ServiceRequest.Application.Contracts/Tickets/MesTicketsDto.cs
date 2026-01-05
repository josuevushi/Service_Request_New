using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.Tickets
{
    public class MesTicketsDto 
    {
    public Guid Id { get; set; }

    public string Type { get; set; }
    public string Famille { get; set; }
    public string Numero { get; set; }
    public string Status { get; set; }
    public string Objet { get; set; }
    public string User { get; set; }
    public string Groupe { get; set; }


    public DateTime CreationDate { get; set; }
    public DateTime ClosureDate { get; set; }

     public DateTime? StartDate { get; set; }
    public DateTime? PendingDate { get; set; }
    public DateTime? EstimateDate { get; set; }
    public  double Pourcentage { get; set; }
    }
}