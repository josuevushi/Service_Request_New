using Volo.Abp.Modularity;

namespace Dn.ServiceRequest;

/* Inherit from this class for your domain layer tests. */
public abstract class ServiceRequestDomainTestBase<TStartupModule> : ServiceRequestTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
