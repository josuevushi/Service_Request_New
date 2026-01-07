using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dn.ServiceRequest.Familles;
using Dn.ServiceRequest.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Dn.ServiceRequest.Permissions;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.Web.Pages.Admin
{
    [Authorize(ServiceRequestPermissions.Types.Default)]
    public class TypeDemandeAdminModel : ServiceRequestPageModel
    {
        private readonly ITypeAppService _typeAppService;
        private readonly IFamilleAppService _familleAppService;

        public List<TypeDto> Types { get; set; } = new();
        public List<SelectListItem> Familles { get; set; } = new();

        [BindProperty]
        public CreateUpdateTypeDto NewType { get; set; } = new();

        [BindProperty]
        public CreateUpdateTypeDto Type { get; set; } = new();

        [BindProperty]
        public Guid EditTypeId { get; set; }

        public TypeDemandeAdminModel(ITypeAppService typeAppService, IFamilleAppService familleAppService)
        {
            _typeAppService = typeAppService;
            _familleAppService = familleAppService;
        }

        public async Task OnGetAsync()
        {
            var typeResult = await _typeAppService.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
            Types = new List<TypeDto>(typeResult.Items);

            var familleResult = await _familleAppService.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
            Familles = familleResult.Items.Select(f => new SelectListItem(f.Nom, f.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            await _typeAppService.CreateAsync(NewType);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditOrUpdateAsync()
        {
            await _typeAppService.UpdateAsync(EditTypeId, Type);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _typeAppService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
