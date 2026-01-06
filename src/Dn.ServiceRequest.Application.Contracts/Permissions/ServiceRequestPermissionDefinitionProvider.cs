using Dn.ServiceRequest.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dn.ServiceRequest.Permissions;

public class ServiceRequestPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ServiceRequestPermissions.GroupName, L("Permission:ServiceRequest"));

        var ticketsPermission = myGroup.AddPermission(ServiceRequestPermissions.Tickets.Default, L("Permission:Tickets"));
        
        var traiterPermission = ticketsPermission.AddChild(ServiceRequestPermissions.Tickets.Traiter, L("Permission:TraiterTicket"));
        traiterPermission.AddChild(ServiceRequestPermissions.Tickets.Start, L("Permission:Start"));
        traiterPermission.AddChild(ServiceRequestPermissions.Tickets.Pending, L("Permission:Pending"));
        traiterPermission.AddChild(ServiceRequestPermissions.Tickets.Close, L("Permission:Close"));
        traiterPermission.AddChild(ServiceRequestPermissions.Tickets.Transfert, L("Permission:Transfert"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ServiceRequestResource>(name);
    }
}
