using NewLife.Configuration;
using SageKing.Application.SqlSugar.Features;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseSageKingApplicationSqlSugar(this IModule module, Action<SageKingApplicationSqlSugarFeature>? configure = default)
    {
        //当前程序集
        var currAssembly = typeof(SageKingApplicationSqlSugarFeature).Assembly;

        module.Configure<SageKingApplicationSqlSugarFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(currAssembly);
        });

        module.UseSageKingDatabaseSqlSugar(o => o.ClientTypeDicOptions += a =>
        {
            var SqlSugarConst = a.SqlSugarDefault;
            a.InitDatabaseAction += (db, config) =>
            {
                SqlSugarScopeProvider dbProvider = db.GetConnectionScope(config.ConfigId);

                // 初始化/创建数据库
                if (config.DbSettings.EnableInitDb)
                {
                    if (config.DbType != SqlSugar.DbType.Oracle)
                        dbProvider.DbMaintenance.CreateDatabase();
                }
                IEnumerable<Type> currAss = null;


                // 初始化表结构
                if (config.TableSettings.EnableInitTable)
                {
                    currAss ??= currAssembly.GetTypesSuppressSniffer(typeof(SuppressSnifferAttribute));
                    var entityTypes = currAss.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass && u.IsDefined(typeof(SugarTable), false))
                    .Where(u => !u.GetCustomAttributes<IgnoreTableAttribute>().Any())
                        .WhereIF(config.TableSettings.EnableIncreTable, u => u.IsDefined(typeof(IncreTableAttribute), false)).ToList();

                    if (config.ConfigId.ToString() == SqlSugarConst.MainConfigId) // 默认库（有系统表特性、没有日志表和租户表特性）
                        entityTypes = entityTypes.Where(u => u.GetCustomAttributes<SysTableAttribute>().Any() || (!u.GetCustomAttributes<LogTableAttribute>().Any() && !u.GetCustomAttributes<TenantAttribute>().Any())).ToList();
                    else if (config.ConfigId.ToString() == SqlSugarConst.LogConfigId) // 日志库
                        entityTypes = entityTypes.Where(u => u.GetCustomAttributes<LogTableAttribute>().Any()).ToList();
                    else
                        entityTypes = entityTypes.Where(u => u.GetCustomAttribute<TenantAttribute>()?.configId.ToString() == config.ConfigId.ToString()).ToList(); // 自定义的库

                    foreach (var entityType in entityTypes)
                    {
                        if (entityType.GetCustomAttribute<SplitTableAttribute>() == null)
                            dbProvider.CodeFirst.InitTables(entityType);
                        else
                            dbProvider.CodeFirst.SplitTables().InitTables(entityType);
                    }
                }

                // 初始化种子数据
                if (config.SeedSettings.EnableInitSeed)
                {
                    currAss ??= currAssembly.GetTypesSuppressSniffer(typeof(SuppressSnifferAttribute));
                    var seedDataTypes = currAss.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass && u.GetInterfaces().Any(i => i.HasImplementedRawGeneric(typeof(ISqlSugarEntitySeedData<>))))
                        .WhereIF(config.SeedSettings.EnableIncreSeed, u => u.IsDefined(typeof(IncreSeedAttribute), false))
                        .OrderBy(u => u.GetCustomAttributes(typeof(SeedDataAttribute), false).Length > 0 ? (u.GetCustomAttributes(typeof(SeedDataAttribute), false)[0] as SeedDataAttribute).Order : 0).ToList();

                    foreach (var seedType in seedDataTypes)
                    {
                        var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();
                        if (config.ConfigId.ToString() == SqlSugarConst.MainConfigId) // 默认库（有系统表特性、没有日志表和租户表特性）
                        {
                            if (entityType.GetCustomAttribute<SysTableAttribute>() == null && (entityType.GetCustomAttribute<LogTableAttribute>() != null || entityType.GetCustomAttribute<TenantAttribute>() != null))
                                continue;
                        }
                        else if (config.ConfigId.ToString() == SqlSugarConst.LogConfigId) // 日志库
                        {
                            if (entityType.GetCustomAttribute<LogTableAttribute>() == null)
                                continue;
                        }
                        else
                        {
                            var att = entityType.GetCustomAttribute<TenantAttribute>(); // 自定义的库
                            if (att == null || att.configId.ToString() != config.ConfigId.ToString()) continue;
                        }

                        var instance = Activator.CreateInstance(seedType);
                        var hasDataMethod = seedType.GetMethod("HasData");
                        var seedData = ((IEnumerable)hasDataMethod?.Invoke(instance, null))?.Cast<object>();
                        if (seedData == null) continue;

                        var entityInfo = dbProvider.EntityMaintenance.GetEntityInfo(entityType);
                        if (entityInfo.Columns.Any(u => u.IsPrimarykey))
                        {
                            // 按主键进行批量增加和更新
                            var storage = dbProvider.StorageableByObject(seedData.ToList()).ToStorage();
                            storage.AsInsertable.ExecuteCommand();
                            if (seedType.GetCustomAttribute<IgnoreUpdateSeedAttribute>() == null) // 有忽略更新种子特性时则不更新
                                storage.AsUpdateable.IgnoreColumns(entityInfo.Columns.Where(c => c.PropertyInfo.GetCustomAttribute<IgnoreUpdateSeedColumnAttribute>() != null).Select(c => c.PropertyName).ToArray()).ExecuteCommand();
                        }
                        else
                        {
                            // 无主键则只进行插入
                            if (!dbProvider.Queryable(entityInfo.DbTableName, entityInfo.DbTableName).Any())
                                dbProvider.InsertableByObject(seedData.ToList()).ExecuteCommand();
                        }
                    }
                }
            };
        });
        return module;
    }
}
