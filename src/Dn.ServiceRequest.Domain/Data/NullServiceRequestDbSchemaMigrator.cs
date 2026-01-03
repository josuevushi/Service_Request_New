using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dn.ServiceRequest.Data;

/* This is used if database provider does't define
 * IServiceRequestDbSchemaMigrator implementation.
 */
public class NullServiceRequestDbSchemaMigrator : IServiceRequestDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
