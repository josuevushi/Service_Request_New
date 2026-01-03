using Dn.ServiceRequest.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dn.ServiceRequest.Permissions;

public class ServiceRequestPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ServiceRequestPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ServiceRequestPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ServiceRequestResource>(name);
    }
}
