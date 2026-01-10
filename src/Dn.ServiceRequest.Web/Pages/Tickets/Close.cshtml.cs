using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dn.ServiceRequest.Tickets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Volo.Abp.Timing;

using Dn.ServiceRequest.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Dn.ServiceRequest.Web.Pages.Tickets
{
    [Authorize(ServiceRequestPermissions.Tickets.Close)]
    public class Close : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly TicketAppService _ticketAppService;
        private readonly IClock _clock;
        public Guid Id { get; set; }
        public TicketDto Ticket { get; set; }

        public Close(ILogger<Index> logger,TicketAppService ticketAppService,IClock clock)
        {
            _logger = logger;
             _ticketAppService=ticketAppService;
             _clock=clock;
   
        }

          public async Task<IActionResult> OnGetAsync(Guid id)
{
    Id = id;

    // Mise à jour du ticket (clôture et notification)
    await _ticketAppService.CloseTicketAsync(id);

    _logger.LogInformation("Id reçu : {Id}", id);

    // Redirection vers la page précédente
    return Redirect(Request.Headers["Referer"].ToString());
}

    }
}