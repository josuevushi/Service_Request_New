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

            if (remaining.TotalDays >= 1)
            {
                int days = (int)Math.Floor(remaining.TotalDays);
                return $"il vous reste {days} {(days > 1 ? "jours" : "jour")}";
            }
            
            if (remaining.TotalHours >= 1)
            {
                int hours = (int)Math.Floor(remaining.TotalHours);
                int minutes = remaining.Minutes;
                if (minutes > 0)
                {
                    return $"il vous reste {hours} {(hours > 1 ? "heures" : "heure")} et {minutes} {(minutes > 1 ? "minutes" : "minute")}";
                }
                return $"il vous reste {hours} {(hours > 1 ? "heures" : "heure")}";
            }

            int mins = (int)Math.Floor(remaining.TotalMinutes);
            return $"il vous reste {mins} {(mins > 1 ? "minutes" : "minute")}";
        }
    }
}
