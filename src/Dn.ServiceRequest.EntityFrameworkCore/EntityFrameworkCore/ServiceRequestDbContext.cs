
using Dn.ServiceRequest.Familles;
using Dn.ServiceRequest.Groupes;
using Dn.ServiceRequest.Types;
using Dn.ServiceRequest.Tickets;
using Dn.ServiceRequest.PieceJointes;
using Dn.ServiceRequest.Comments;
using Dn.ServiceRequest.GroupeUsers;
using Dn.ServiceRequest.GroupeTypes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Dn.ServiceRequest.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class ServiceRequestDbContext :
    AbpDbContext<ServiceRequestDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<Famille> Familles { get; set; }
    public DbSet<Groupe> Groupes { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<PieceJointe> PieceJointes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<GroupeUser> GroupeUsers { get; set; }
    public DbSet<GroupeType> GroupeTypes { get; set; }








    public ServiceRequestDbContext(DbContextOptions<ServiceRequestDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
            // 👇 CRITIQUE : fixer les préfixes AVANT la configuration des modules
        AbpIdentityDbProperties.DbTablePrefix = "";
        AbpTenantManagementDbProperties.DbTablePrefix = "";
        AbpPermissionManagementDbProperties.DbTablePrefix = "";
        AbpSettingManagementDbProperties.DbTablePrefix = "";
       // AbpAuditLoggingDbProperties.DbTablePreyfix = "";
        AbpBackgroundJobsDbProperties.DbTablePrefix = "";
        AbpFeatureManagementDbProperties.DbTablePrefix = "";
        AbpOpenIddictDbProperties.DbTablePrefix = "";

   base.OnModelCreating(builder);
    
        /* Include modules to your migration db context */
        
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<Famille>(b =>
        {
            b.ToTable( "Familles", ServiceRequestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props7
            b.Property(x => x.Nom).IsRequired().HasMaxLength(128);
        
        });
        //
          builder.Entity<Groupe>(b =>
        {
            b.ToTable( "Groupes", ServiceRequestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props7
            b.Property(x => x.Nom).IsRequired().HasMaxLength(128);
        
        });
           builder.Entity<Type>(b =>
        {
            b.ToTable( "Types", ServiceRequestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props7
            b.Property(x => x.Json_form).IsRequired().HasMaxLength(128);
        
        });
           builder.Entity<GroupeUser>(b =>
        {
            b.ToTable( "GroupeUsers", ServiceRequestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props7
            b.Property(x => x.Groupe_id).IsRequired();
        
        });
          builder.Entity<GroupeType>(b =>
        {
            b.ToTable( "GroupeTypes", ServiceRequestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props7
            b.Property(x => x.Groupe_id).IsRequired();
        
        });
        
    }
}
