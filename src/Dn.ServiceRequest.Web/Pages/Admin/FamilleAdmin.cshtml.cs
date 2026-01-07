using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dn.ServiceRequest.Familles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dn.ServiceRequest.Permissions;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.Web.Pages.Admin
{
    [Authorize(ServiceRequestPermissions.Familles.Default)]
    public class FamilleAdminModel : ServiceRequestPageModel
    {
        private readonly IFamilleAppService _familleAppService;

        public List<FamilleDto> Familles { get; set; } = new();

        [BindProperty]
        public CreateUpdateFamilleDto NewFamille { get; set; } = new();

        [BindProperty]
        public CreateUpdateFamilleDto Famille { get; set; } = new();

        [BindProperty]
        public Guid EditFamilleId { get; set; }

        public FamilleAdminModel(IFamilleAppService familleAppService)
        {
            _familleAppService = familleAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _familleAppService.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
            Familles = new List<FamilleDto>(result.Items);
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            await _familleAppService.CreateAsync(NewFamille);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditOrUpdateAsync()
        {
            await _familleAppService.UpdateAsync(EditFamilleId, Famille);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _familleAppService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
