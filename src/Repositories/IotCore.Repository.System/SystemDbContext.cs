using FreeSql;
using IotCore.Repository.System.Models;
using Microsoft.Extensions.Configuration;

namespace IotCore.Repository.System
{
    public partial class SystemDbContext : FreeSqlDbContext
    {
        public SystemDbContext(IConfiguration configuration) : base(configuration.GetSection("DbOptions:System"),
            builder =>
            {
                builder.UseAutoSyncStructure(true);
            })
        {
        }

        public SystemDbContext(IFreeSql freeSql) : base(freeSql)
        {
        }
    }
}
