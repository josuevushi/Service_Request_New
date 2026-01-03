using Dn.ServiceRequest.Samples;
using Xunit;

namespace Dn.ServiceRequest.EntityFrameworkCore.Domains;

[Collection(ServiceRequestTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ServiceRequestEntityFrameworkCoreTestModule>
{

}
