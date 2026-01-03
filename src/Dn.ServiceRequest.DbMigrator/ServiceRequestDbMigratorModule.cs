using Dn.ServiceRequest.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dn.ServiceRequest.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ServiceRequestEntityFrameworkCoreModule),
    typeof(ServiceRequestApplicationContractsModule)
    )]
public class ServiceRequestDbMigratorModule : AbpModule
{
}
