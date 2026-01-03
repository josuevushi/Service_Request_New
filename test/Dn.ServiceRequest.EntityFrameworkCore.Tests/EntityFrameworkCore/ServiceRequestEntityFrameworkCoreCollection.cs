using Xunit;

namespace Dn.ServiceRequest.EntityFrameworkCore;

[CollectionDefinition(ServiceRequestTestConsts.CollectionDefinitionName)]
public class ServiceRequestEntityFrameworkCoreCollection : ICollectionFixture<ServiceRequestEntityFrameworkCoreFixture>
{

}
