using Dn.ServiceRequest.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Dn.ServiceRequest.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ServiceRequestPageModel : AbpPageModel
{
    protected ServiceRequestPageModel()
    {
        LocalizationResourceType = typeof(ServiceRequestResource);
    }
}
