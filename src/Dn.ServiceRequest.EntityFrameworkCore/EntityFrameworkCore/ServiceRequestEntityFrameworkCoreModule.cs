using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.OpenIddict;

namespace Dn.ServiceRequest.EntityFrameworkCore;

[DependsOn(
    typeof(ServiceRequestDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class ServiceRequestEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    
    {
     ServiceRequestEfCoreEntityExtensionMappings.Configure();

        AbpIdentityDbProperties.DbTablePrefix = "";
        AbpTenantManagementDbProperties.DbTablePrefix = "";
        AbpPermissionManagementDbProperties.DbTablePrefix = "";
        AbpSettingManagementDbProperties.DbTablePrefix = "";
       // AbpAuditLoggingDbProperties.DbTablePrefix = "";
        AbpBackgroundJobsDbProperties.DbTablePrefix = "";
        AbpFeatureManagementDbProperties.DbTablePrefix = "";
        AbpOpenIddictDbProperties.DbTablePrefix = "";

    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
            AbpIdentityDbProperties.DbTablePrefix = "";
        AbpTenantManagementDbProperties.DbTablePrefix = "";
        AbpPermissionManagementDbProperties.DbTablePrefix = "";
        AbpSettingManagementDbProperties.DbTablePrefix = "";
       // AbpAuditLoggingDbProperties.DbTablePrefix = "";
        AbpBackgroundJobsDbProperties.DbTablePrefix = "";
        AbpFeatureManagementDbProperties.DbTablePrefix = "";
        AbpOpenIddictDbProperties.DbTablePrefix = "";

        context.Services.AddAbpDbContext<ServiceRequestDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also ServiceRequestMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });
        

    }
}
