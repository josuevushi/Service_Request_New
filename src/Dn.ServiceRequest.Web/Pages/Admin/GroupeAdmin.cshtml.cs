using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dn.ServiceRequest.Groupes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dn.ServiceRequest.Permissions;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.Web.Pages.Admin
{
    [Authorize(ServiceRequestPermissions.Groupes.Default)]
    public class GroupeAdminModel : ServiceRequestPageModel
    {
        private readonly IGroupeAppService _groupeAppService;

        public List<GroupeDto> Groupes { get; set; } = new();

        [BindProperty]
        public CreateUpdateGroupeDto NewGroupe { get; set; } = new();

        [BindProperty]
        public CreateUpdateGroupeDto Groupe { get; set; } = new();

        [BindProperty]
        public Guid EditGroupeId { get; set; }

        public GroupeAdminModel(IGroupeAppService groupeAppService)
        {
            _groupeAppService = groupeAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _groupeAppService.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
            Groupes = new List<GroupeDto>(result.Items);
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            await _groupeAppService.CreateAsync(NewGroupe);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditOrUpdateAsync()
        {
            await _groupeAppService.UpdateAsync(EditGroupeId, Groupe);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _groupeAppService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
