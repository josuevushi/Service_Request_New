using Volo.Abp.Modularity;

namespace Dn.ServiceRequest;

public abstract class ServiceRequestApplicationTestBase<TStartupModule> : ServiceRequestTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
