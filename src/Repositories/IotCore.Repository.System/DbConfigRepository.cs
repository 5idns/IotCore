using System;
using System.Threading.Tasks;
using IotCore.Enumerations;
using IotCore.Exceptions;
using IotCore.Repository.System.Models;

namespace IotCore.Repository.System
{
    public class DbConfigRepository : CommonRepository<SystemDbContext, DbConfig>
    {
        public DbConfigRepository(SystemDbContext context) : base(context)
        {
        }

        public DbConfig[] GetAll()
        {
            try
            {
                var dbConfigs = Where(f => f.IsEnable).ToList();
                return dbConfigs.ToArray();
            }
            catch (Exception e)
            {
                throw new SysException(Status.DatabaseError, "加载租户数据时发生错误", e);
            }
        }

        public async Task<DbConfig[]> GetAllAsync()
        {
            try
            {
                var dbConfigs = await Where(f => f.IsEnable).ToListAsync();
                return dbConfigs.ToArray();
            }
            catch (Exception e)
            {
                throw new SysException(Status.DatabaseError, "加载租户数据时发生错误", e);
            }
        }
    }
}
