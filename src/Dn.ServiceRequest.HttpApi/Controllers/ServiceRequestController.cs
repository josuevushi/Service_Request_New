using Dn.ServiceRequest.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dn.ServiceRequest.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ServiceRequestController : AbpControllerBase
{
    protected ServiceRequestController()
    {
        LocalizationResource = typeof(ServiceRequestResource);
    }
}
