using Microsoft.AspNetCore.Builder;
using Dn.ServiceRequest;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Dn.ServiceRequest.Web.csproj");
await builder.RunAbpModuleAsync<ServiceRequestWebTestModule>(applicationName: "Dn.ServiceRequest.Web" );

public partial class Program
{
}
