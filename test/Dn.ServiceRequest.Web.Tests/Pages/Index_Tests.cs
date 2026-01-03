using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dn.ServiceRequest.Pages;

public class Index_Tests : ServiceRequestWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
