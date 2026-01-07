using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dn.ServiceRequest.Groupes;
using Dn.ServiceRequest.GroupeTypes;
using Dn.ServiceRequest.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Dn.ServiceRequest.Permissions;
using Volo.Abp.Application.Dtos;

namespace Dn.ServiceRequest.Web.Pages.Admin
{
    [Authorize(ServiceRequestPermissions.GroupeTypes.Default)]
    public class GroupeTypeAdminModel : ServiceRequestPageModel
    {
        private readonly IGroupeTypeAppService _groupeTypeAppService;
        private readonly IGroupeAppService _groupeAppService;
        private readonly ITypeAppService _typeAppService;

        public List<GroupeTypeDto> GroupeTypes { get; set; } = new();
        public List<SelectListItem> Groupes { get; set; } = new();
        public List<SelectListItem> Types { get; set; } = new();

        [BindProperty]
        public CreateUpdateGroupeTypeDto NewGroupeType { get; set; } = new();

        [BindProperty]
        public CreateUpdateGroupeTypeDto GroupeType { get; set; } = new();

        [BindProperty]
        public Guid EditGroupeTypeId { get; set; }

        public GroupeTypeAdminModel(
            IGroupeTypeAppService groupeTypeAppService,
            IGroupeAppService groupeAppService,
            ITypeAppService typeAppService)
        {
            _groupeTypeAppService = groupeTypeAppService;
            _groupeAppService = groupeAppService;
            _typeAppService = typeAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _groupeTypeAppService.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
            GroupeTypes = new List<GroupeTypeDto>(result.Items);

            var groupes = await _groupeAppService.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
            Groupes = groupes.Items.Select(g => new SelectListItem(g.Nom, g.Id.ToString())).ToList();

            var types = await _typeAppService.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
            Types = types.Items.Select(t => new SelectListItem(t.Nom, t.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            await _groupeTypeAppService.CreateAsync(NewGroupeType);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditOrUpdateAsync()
        {
            await _groupeTypeAppService.UpdateAsync(EditGroupeTypeId, GroupeType);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _groupeTypeAppService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
