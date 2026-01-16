using System;
using System.Threading.Tasks;
using Dn.ServiceRequest.Tickets;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dn.ServiceRequest.Web.Pages.Tickets
{
    public class Edit : PageModel
    {
        private readonly ILogger<Edit> _logger;
        private readonly TicketAppService _ticketAppService;

        public Guid Id { get; set; }
        public UnTicketsDto Ticket { get; set; }
        public string RemainingTimeMessage { get; set; }

        public Edit(ILogger<Edit> logger, TicketAppService ticketAppService)
        {
            _logger = logger;
            _ticketAppService = ticketAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            Id = id;
            Ticket = await _ticketAppService.GetUnTicketAsync(id.ToString());
            if (Ticket == null)
            {
                _logger.LogWarning("Ticket non trouvé pour l'Id : {Id}", id);
                return;
            }
            RemainingTimeMessage = GetRemainingTime(Ticket);
            _logger.LogInformation("Id reçu : {Id}", id);
        }

        private string GetRemainingTime(UnTicketsDto ticket)
        {
            if (ticket == null || ticket.EstimateDate == null) return string.Empty;
            if (ticket.Status != "Open" && ticket.Status != "WorkInProgress") return string.Empty;

            var remaining = ticket.EstimateDate.Value - DateTime.Now;

            if (remaining <= TimeSpan.Zero)
            {
                return "Délai dépassé";
            }

            var parts = new System.Collections.Generic.List<string>();
            if (remaining.Days > 0)
                parts.Add($"{remaining.Days} {(remaining.Days > 1 ? "jours" : "jour")}");
            
            if (remaining.Hours > 0)
                parts.Add($"{remaining.Hours} {(remaining.Hours > 1 ? "heures" : "heure")}");
            
            if (remaining.Minutes > 0)
                parts.Add($"{remaining.Minutes} {(remaining.Minutes > 1 ? "minutes" : "minute")}");

            if (parts.Count == 0)
            {
                return "il vous reste moins d'une minute";
            }

            if (parts.Count == 1)
            {
                return "il vous reste " + parts[0];
            }

            var finalPart = parts[parts.Count - 1];
            parts.RemoveAt(parts.Count - 1);
            
            return "il vous reste " + string.Join(", ", parts) + " et " + finalPart;
        }
    }
}
