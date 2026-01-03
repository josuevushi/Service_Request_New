using Volo.Abp.Modularity;

namespace Dn.ServiceRequest;

[DependsOn(
    typeof(ServiceRequestDomainModule),
    typeof(ServiceRequestTestBaseModule)
)]
public class ServiceRequestDomainTestModule : AbpModule
{

}
