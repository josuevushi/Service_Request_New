using Dn.ServiceRequest.Samples;
using Xunit;

namespace Dn.ServiceRequest.EntityFrameworkCore.Applications;

[Collection(ServiceRequestTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ServiceRequestEntityFrameworkCoreTestModule>
{

}
