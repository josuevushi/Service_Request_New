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

        var familles = myGroup.AddPermission(ServiceRequestPermissions.Familles.Default, L("Permission:Familles"));
        familles.AddChild(ServiceRequestPermissions.Familles.Create, L("Permission:Create"));
        familles.AddChild(ServiceRequestPermissions.Familles.Update, L("Permission:Update"));
        familles.AddChild(ServiceRequestPermissions.Familles.Delete, L("Permission:Delete"));

        var types = myGroup.AddPermission(ServiceRequestPermissions.Types.Default, L("Permission:Types"));
        types.AddChild(ServiceRequestPermissions.Types.Create, L("Permission:Create"));
        types.AddChild(ServiceRequestPermissions.Types.Update, L("Permission:Update"));
        types.AddChild(ServiceRequestPermissions.Types.Delete, L("Permission:Delete"));

        var groupes = myGroup.AddPermission(ServiceRequestPermissions.Groupes.Default, L("Permission:Groupes"));
        groupes.AddChild(ServiceRequestPermissions.Groupes.Create, L("Permission:Create"));
        groupes.AddChild(ServiceRequestPermissions.Groupes.Update, L("Permission:Update"));
        groupes.AddChild(ServiceRequestPermissions.Groupes.Delete, L("Permission:Delete"));

        var groupeUsers = myGroup.AddPermission(ServiceRequestPermissions.GroupeUsers.Default, L("Permission:GroupeUsers"));
        groupeUsers.AddChild(ServiceRequestPermissions.GroupeUsers.Create, L("Permission:Create"));
        groupeUsers.AddChild(ServiceRequestPermissions.GroupeUsers.Update, L("Permission:Update"));
        groupeUsers.AddChild(ServiceRequestPermissions.GroupeUsers.Delete, L("Permission:Delete"));

        var groupeTypes = myGroup.AddPermission(ServiceRequestPermissions.GroupeTypes.Default, L("Permission:GroupeTypes"));
        groupeTypes.AddChild(ServiceRequestPermissions.GroupeTypes.Create, L("Permission:Create"));
        groupeTypes.AddChild(ServiceRequestPermissions.GroupeTypes.Update, L("Permission:Update"));
        groupeTypes.AddChild(ServiceRequestPermissions.GroupeTypes.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ServiceRequestResource>(name);
    }
}
