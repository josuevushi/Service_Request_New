using System;
using System.Threading.Tasks;
using Dn.ServiceRequest.Tickets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dn.ServiceRequest.Web.Pages.Tickets
{
    [IgnoreAntiforgeryToken]
    public class Save : PageModel
    {
        private readonly ILogger<Save> _logger;
        private readonly TicketAppService _ticketAppService;

        public Save(ILogger<Save> logger, TicketAppService ticketAppService)
        {
            _logger = logger;
            _ticketAppService = ticketAppService;
        }

        public async Task<IActionResult> OnPostAsync(Guid id, Guid assignedTo)
        {
            try
            {
                var ticket = await _ticketAppService.GetAsync(id);
                
                var updateDto = new CreateUpdateTicketDto
                {
                    Object = ticket.Object,
                    Description = ticket.Description,
                    Json_form = ticket.Json_form,
                    Type_id = ticket.Type_id,
                    Numero = ticket.Numero,
                    Status = ticket.Status,
                    AssignedTo = assignedTo,
                    ClosureDate = ticket.ClosureDate,
                    StartDate = ticket.StartDate,
                    PendingDate = ticket.PendingDate,
                    EstimateDate = ticket.EstimateDate
                };

                await _ticketAppService.UpdateAsync(id, updateDto);

                _logger.LogInformation("Ticket {Id} updated with AssignTo: {AssignTo}", id, assignedTo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ticket {Id}", id);
            }

            return Redirect(Request.Headers["Referer"].ToString() ?? "/Tickets");
        }
    }
}
