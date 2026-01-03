using System;
using System.Collections.Generic;
using System.Text;
using Dn.ServiceRequest.Localization;
using Volo.Abp.Application.Services;

namespace Dn.ServiceRequest;

/* Inherit your application services from this class.
 */
public abstract class ServiceRequestAppService : ApplicationService
{
    protected ServiceRequestAppService()
    {
        LocalizationResource = typeof(ServiceRequestResource);
    }
}
