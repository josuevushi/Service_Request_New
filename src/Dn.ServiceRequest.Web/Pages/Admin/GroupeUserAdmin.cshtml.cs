using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dn.ServiceRequest.Groupes;
using Dn.ServiceRequest.GroupeUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Dn.ServiceRequest.Permissions;
using Volo.Abp.Identity;

namespace Dn.ServiceRequest.Web.Pages.Admin
{
    [Authorize(ServiceRequestPermissions.GroupeUsers.Default)]
    public class GroupeUserAdminModel : ServiceRequestPageModel
    {
        private readonly IGroupeUserAppService _groupeUserAppService;
        private readonly IGroupeAppService _groupeAppService;
        private readonly IIdentityUserAppService _identityUserAppService;

        public List<GroupeUserDto> GroupeUsers { get; set; } = new();
        public List<SelectListItem> Groupes { get; set; } = new();
        public List<SelectListItem> Users { get; set; } = new();

        [BindProperty]
        public CreateUpdateGroupeUserDto NewGroupeUser { get; set; } = new();

        [BindProperty]
        public CreateUpdateGroupeUserDto GroupeUser { get; set; } = new();

        [BindProperty]
        public Guid EditGroupeUserId { get; set; }

        public GroupeUserAdminModel(
            IGroupeUserAppService groupeUserAppService,
            IGroupeAppService groupeAppService,
            IIdentityUserAppService identityUserAppService)
        {
            _groupeUserAppService = groupeUserAppService;
            _groupeAppService = groupeAppService;
            _identityUserAppService = identityUserAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _groupeUserAppService.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
            GroupeUsers = new List<GroupeUserDto>(result.Items);

            var groupes = await _groupeAppService.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 1000 });
            Groupes = groupes.Items.Select(g => new SelectListItem(g.Nom, g.Id.ToString())).ToList();

            var users = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput { MaxResultCount = 1000 });
            Users = users.Items.Select(u => new SelectListItem(u.Email, u.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            await _groupeUserAppService.CreateAsync(NewGroupeUser);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditOrUpdateAsync()
        {
            await _groupeUserAppService.UpdateAsync(EditGroupeUserId, GroupeUser);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _groupeUserAppService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
