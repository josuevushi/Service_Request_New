using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Identity;
using System.Linq;

namespace Dn.ServiceRequest.Web.Pages.Admin;

[Authorize(IdentityPermissions.Users.Default)]
public class UserAdminModel : ServiceRequestPageModel
{
    private readonly IIdentityUserAppService _identityUserAppService;
    private readonly IIdentityRoleAppService _identityRoleAppService;

    public IReadOnlyList<IdentityUserDto> Users { get; set; } = new List<IdentityUserDto>();
    public IReadOnlyList<IdentityRoleDto> Roles { get; set; } = new List<IdentityRoleDto>();
    public Dictionary<Guid, IReadOnlyList<string>> UserRoles { get; set; } = new();

    [BindProperty]
    public IdentityUserCreateDto NewUser { get; set; } = new();

    [BindProperty]
    public IdentityUserUpdateDto user { get; set; } = new();

    [BindProperty]
    public Guid EditUserId { get; set; }

    public UserAdminModel(
        IIdentityUserAppService identityUserAppService,
        IIdentityRoleAppService identityRoleAppService)
    {
        _identityUserAppService = identityUserAppService;
        _identityRoleAppService = identityRoleAppService;
    }

    public async Task OnGetAsync()
    {
        var userResult = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput { MaxResultCount = 1000 });
        Users = userResult.Items;

        var roleResult = await _identityRoleAppService.GetListAsync(new GetIdentityRolesInput { MaxResultCount = 1000 });
        Roles = roleResult.Items;

        foreach (var u in Users)
        {
            var userRoles = await _identityUserAppService.GetRolesAsync(u.Id);
            UserRoles[u.Id] = userRoles.Items.Select(r => r.Name).ToList();
        }
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (string.IsNullOrEmpty(NewUser.Password))
        {
            NewUser.Password = "DefaultPassword123!"; // Set a default password if not provided
        }
        await _identityUserAppService.CreateAsync(NewUser);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditOrUpdateAsync()
    {
        await _identityUserAppService.UpdateAsync(EditUserId, user);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        await _identityUserAppService.DeleteAsync(id);
        return RedirectToPage();
    }
}
