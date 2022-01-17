﻿using System;
using System.Collections.Generic;
using System.Reflection;
using FreeSql;
using FreeSql.DataAnnotations;
using IotCore.Repository.Configurations;
using Microsoft.Extensions.Configuration;

namespace IotCore.Repository
{
    public abstract class FreeSqlDbContext : DbContext
    {
        protected FreeSqlDbContext(IConfiguration configuration) : this(configuration, null)
        {

        }
        protected FreeSqlDbContext(IConfiguration configuration, Action<FreeSqlBuilder> builder)
        {
            var dbOptions = configuration.Get<DbOptions>();

            IFreeSql CreateFreeSql()
            {
                var freeSqlBuilder = new FreeSqlBuilder()
                    .UseConnectionString(dbOptions.DataType, dbOptions.ConnectionString);
                if (builder != null)
                {
                    builder(freeSqlBuilder);
                }

                var newFreeSql = freeSqlBuilder.Build();
                newFreeSql.UseJsonMap();
                return newFreeSql;
            }

            DbContext = CreateFreeSql();
        }

        protected FreeSqlDbContext(IFreeSql freeSql)
        {
            DbContext = freeSql;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseFreeSql(DbContext);
        }

        protected override void OnModelCreating(ICodeFirst codefirst)
        {

        }

        public IFreeSql DbContext { get; }

        /// <summary>
        /// 同步数据库实体
        /// </summary>
        /// <param name="types">实体类型</param>
        /// <returns></returns>
        public FreeSqlDbContext SyncStructure(params Type[] types)
        {
            DbContext.CodeFirst.SyncStructure(types);
            return this;
        }

        /// <summary>
        /// 同步数据库实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public FreeSqlDbContext SyncStructure<TEntity>()
        {
            DbContext.CodeFirst.SyncStructure<TEntity>();
            return this;
        }

        private static Type[] GetTypesByTableAttribute(Assembly[] assemblies)
        {
            List<Type> tableAssembies = new List<Type>();
            foreach (Assembly assembly in assemblies)
                foreach (Type type in assembly.GetExportedTypes())
                    foreach (Attribute attribute in type.GetCustomAttributes())
                        if (attribute is TableAttribute tableAttribute)
                            if (tableAttribute.DisableSyncStructure == false)
                                tableAssembies.Add(type);

            return tableAssembies.ToArray();
        }

        public FreeSqlDbContext SyncStructure(params Assembly[] assemblies)
        {
            var types = GetTypesByTableAttribute(assemblies);
            DbContext.CodeFirst.SyncStructure(types);
            return this;
        }
    }
}
