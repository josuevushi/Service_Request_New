using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dn.ServiceRequest.Tickets;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Dn.ServiceRequest.Permissions;

namespace Dn.ServiceRequest.Web.Pages.Tickets
{
    [Authorize(ServiceRequestPermissions.Tickets.GlobalSearch)]
    public class GlobalSearchModel : ServiceRequestPageModel
    {
        private readonly ITicketAppService _ticketAppService;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<MesTicketsDto> Tickets { get; set; }

        public GlobalSearchModel(ITicketAppService ticketAppService)
        {
            _ticketAppService = ticketAppService;
            Tickets = new List<MesTicketsDto>();
        }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                Tickets = await _ticketAppService.GlobalSearchAsync(SearchTerm);
            }
        }
    }
}
