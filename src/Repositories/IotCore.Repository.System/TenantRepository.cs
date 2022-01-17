using IotCore.Repository.System.Models;

namespace IotCore.Repository.System
{
    public class TenantRepository : CommonRepository<SystemDbContext, Tenant>
    {
        public TenantRepository(SystemDbContext context) : base(context)
        {
        }
    }
}
