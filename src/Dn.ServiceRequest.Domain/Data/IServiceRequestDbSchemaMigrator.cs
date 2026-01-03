using System.Threading.Tasks;

namespace Dn.ServiceRequest.Data;

public interface IServiceRequestDbSchemaMigrator
{
    Task MigrateAsync();
}
