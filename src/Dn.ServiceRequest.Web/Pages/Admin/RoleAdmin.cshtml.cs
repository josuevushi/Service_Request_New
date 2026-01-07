using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace Dn.ServiceRequest.Web.Pages.Admin;

[Authorize(IdentityPermissions.Roles.Default)]
public class RoleAdminModel : ServiceRequestPageModel
{
    private readonly IIdentityRoleAppService _identityRoleAppService;
    private readonly IPermissionAppService _permissionAppService;

    public IReadOnlyList<IdentityRoleDto> Roles { get; set; } = new List<IdentityRoleDto>();

    [BindProperty]
    public IdentityRoleCreateDto NewRole { get; set; } = new();

    [BindProperty]
    public IdentityRoleUpdateDto EditRole { get; set; } = new();

    [BindProperty]
    public Guid EditRoleId { get; set; }

    [BindProperty]
    public List<PermissionViewModel> PermissionGroups { get; set; } = new();

    public class PermissionViewModel
    {
        public string GroupName { get; set; }
        public string DisplayName { get; set; }
        public List<PermissionGrantViewModel> Permissions { get; set; } = new();
    }

    public class PermissionGrantViewModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsGranted { get; set; }
        public string? ParentName { get; set; }
    }

    public RoleAdminModel(IIdentityRoleAppService identityRoleAppService, IPermissionAppService permissionAppService)
    {
        _identityRoleAppService = identityRoleAppService;
        _permissionAppService = permissionAppService;
    }

    public async Task OnGetAsync()
    {
        var roleResult = await _identityRoleAppService.GetListAsync(new GetIdentityRolesInput { MaxResultCount = 1000 });
        Roles = roleResult.Items;
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        await _identityRoleAppService.CreateAsync(NewRole);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUpdateAsync()
    {
        await _identityRoleAppService.UpdateAsync(EditRoleId, EditRole);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        await _identityRoleAppService.DeleteAsync(id);
        return RedirectToPage();
    }

    public async Task<JsonResult> OnGetPermissionsAsync(string providerName, string providerKey)
    {
        var permissions = await _permissionAppService.GetAsync(providerName, providerKey);
        var result = permissions.Groups.Select(g => new
        {
            g.Name,
            g.DisplayName,
            Permissions = g.Permissions.Select(p => new
            {
                p.Name,
                p.DisplayName,
                p.IsGranted,
                p.ParentName
            })
        });

        return new JsonResult(result);
    }

    public async Task<IActionResult> OnPostUpdatePermissionsAsync(string providerName, string providerKey, [FromBody] List<UpdatePermissionDto> permissions)
    {
        if (permissions == null)
        {
            return new JsonResult(new { success = false, message = "Permissions data is missing" });
        }

        var updateDto = new UpdatePermissionsDto
        {
            Permissions = permissions.ToArray()
        };

        await _permissionAppService.UpdateAsync(providerName, providerKey, updateDto);
        return new JsonResult(new { success = true });
    }
}
