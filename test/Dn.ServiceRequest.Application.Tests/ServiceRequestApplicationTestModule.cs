using Volo.Abp.Modularity;

namespace Dn.ServiceRequest;

[DependsOn(
    typeof(ServiceRequestApplicationModule),
    typeof(ServiceRequestDomainTestModule)
)]
public class ServiceRequestApplicationTestModule : AbpModule
{

}
