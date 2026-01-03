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

        public Edit(ILogger<Edit> logger, TicketAppService ticketAppService)
        {
            _logger = logger;
            _ticketAppService = ticketAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            Id = id;
            Ticket = await _ticketAppService.GetUnTicketAsync(id.ToString());
            _logger.LogInformation("Id reçu : {Id}", id);
        }
    }
}
