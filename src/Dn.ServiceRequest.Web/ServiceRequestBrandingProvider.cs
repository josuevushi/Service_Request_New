using Microsoft.Extensions.Localization;
using Dn.ServiceRequest.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Dn.ServiceRequest.Web;

[Dependency(ReplaceServices = true)]
public class ServiceRequestBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ServiceRequestResource> _localizer;

    public ServiceRequestBrandingProvider(IStringLocalizer<ServiceRequestResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
