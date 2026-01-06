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
        [Authorize(ServiceRequestPermissions.Tickets.Start)]
        public class Start : PageModel
        {
            private readonly ILogger<Index> _logger;
            private readonly TicketAppService _ticketAppService;
            public TicketDto Ticket { get; set; }
            private readonly IClock _clock;


            public Guid Id { get; set; }
            public Start(ILogger<Index> logger,TicketAppService ticketAppService,IClock clock)
            {
                _logger = logger;
                _ticketAppService=ticketAppService;
                _clock = clock;
            }

            public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Id = id;

        // Récupération du ticket
        Ticket = await _ticketAppService.GetAsync(id);

        // Préparer l'objet pour mise à jour
        var updateDto = new CreateUpdateTicketDto
        {
            Object = Ticket.Object,
            Description = Ticket.Description,
            Json_form=Ticket.Json_form,
            Type_id = Ticket.Type_id,
            Numero = Ticket.Numero,
            Status = Status.WorkInProgress,
            EstimateDate=Ticket.EstimateDate,
            StartDate = _clock.Now,
            ClosureDate=Ticket.ClosureDate,
            PendingDate=Ticket.PendingDate


            
            
        };

        // Mise à jour du ticket
        await _ticketAppService.UpdateAsync(id, updateDto);

        _logger.LogInformation("Id reçu : {Id}", id);

        // Redirection vers la page précédente
        return Redirect(Request.Headers["Referer"].ToString());
    }


        }
    }