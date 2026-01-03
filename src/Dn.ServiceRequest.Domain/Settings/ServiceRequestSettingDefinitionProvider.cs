using Volo.Abp.Settings;

namespace Dn.ServiceRequest.Settings;

public class ServiceRequestSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ServiceRequestSettings.MySetting1));
    }
}
