using Microsoft.AspNetCore.Http;
using NewLife.Configuration;
using SageKing.Application.AspNetCore.SqlSugar.Features;
using SageKing.Application.AspNetCore.SqlSugar.Service;
using SageKing.Database.SqlSugar.AspNetCore;
using SageKing.Database.SqlSugar.Options;
using StackExchange.Profiling;
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
    public static IModule UseSageKingApplicationAspNetCoreSqlSugar(this IModule module, Action<SageKingApplicationAspNetCoreSqlSugarFeature>? configure = default)
    {
        //当前程序集
        var currAssembly = typeof(SageKingApplicationAspNetCoreSqlSugarFeature).Assembly;

        module.Configure<SageKingApplicationAspNetCoreSqlSugarFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(currAssembly);
        });

        module.UseSageKingDatabaseSqlSugar(o => o.ClientTypeDicOptions += a =>
        {
            a.ServiceProvider = o.Services.BuildServiceProvider();

            foreach (var item in a.DBConnection.ConnectionConfigs)
            {
                SetDbConfig(item, a.ServiceProvider);
            };

            var SqlSugarConst = a.SqlSugarDefault;


            a.SqlSugarClientConfigAction += (db) =>
            {
                var idGenerator = a.ServiceProvider.GetService<IIdGenerator>()!;
                var sqlSugarFilter = a.ServiceProvider.GetService<ISqlSugarAspNetCoreFilter>()!;
                var httpContext = a.ServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext;

                foreach (var config in a.DBConnection.ConnectionConfigs)
                {
                    var dbProvider = db.GetConnectionScope(config.ConfigId);
                    SetDbAop(dbProvider, a.DBConnection.EnableConsoleSql, idGenerator, sqlSugarFilter, httpContext, SqlSugarConst);
                }
            };

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

    #region MyRegion
    /// <summary>
    /// 配置连接属性
    /// </summary>
    /// <param name="config"></param>
    public static void SetDbConfig(DbConnectionConfig config, IServiceProvider ServiceProvider)
    {
        var configureExternalServices = new ConfigureExternalServices
        {
            EntityNameService = (type, entity) => // 处理表
            {
                entity.IsDisabledDelete = true; // 禁止删除非 sqlsugar 创建的列
                // 只处理贴了特性[SugarTable]表
                if (!type.GetCustomAttributes<SugarTable>().Any())
                    return;
                if (config.DbSettings.EnableUnderLine && !entity.DbTableName.Contains('_'))
                    entity.DbTableName = UtilMethods.ToUnderLine(entity.DbTableName); // 驼峰转下划线
            },
            EntityService = (type, column) => // 处理列
            {
                // 只处理贴了特性[SugarColumn]列
                if (!type.GetCustomAttributes<SugarColumn>().Any())
                    return;
                if (new NullabilityInfoContext().Create(type).WriteState is NullabilityState.Nullable)
                    column.IsNullable = true;
                if (config.DbSettings.EnableUnderLine && !column.IsIgnore && !column.DbColumnName.Contains('_'))
                    column.DbColumnName = UtilMethods.ToUnderLine(column.DbColumnName); // 驼峰转下划线
            },
            DataInfoCacheService = ServiceProvider.GetRequiredService<SqlSugarCache>()
        };
        config.ConfigureExternalServices = configureExternalServices;
        config.InitKeyType = InitKeyType.Attribute;
        config.IsAutoCloseConnection = true;
        config.MoreSettings = new ConnMoreSettings
        {
            IsAutoRemoveDataCache = true, // 启用自动删除缓存，所有增删改会自动调用.RemoveDataCache()
            IsAutoDeleteQueryFilter = true, // 启用删除查询过滤器
            IsAutoUpdateQueryFilter = true, // 启用更新查询过滤器
            SqlServerCodeFirstNvarchar = true // 采用Nvarchar
        };
    }

    /// <summary>
    /// 配置Aop
    /// </summary>
    /// <param name="db"></param>
    /// <param name="enableConsoleSql"></param>
    public static void SetDbAop(SqlSugarScopeProvider db, bool enableConsoleSql, IIdGenerator idGenerator, ISqlSugarAspNetCoreFilter sqlSugarFilter, HttpContext httpContext, SqlSugarDefaultSet SqlSugarConst)
    {
        // 设置超时时间
        db.Ado.CommandTimeOut = 30;

        // 打印SQL语句
        if (enableConsoleSql)
        {
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //// 若参数值超过100个字符则进行截取
                //foreach (var par in pars)
                //{
                //    if (par.DbType != System.Data.DbType.String || par.Value == null) continue;
                //    if (par.Value.ToString().Length > 100)
                //        par.Value = string.Concat(par.Value.ToString()[..100], "......");
                //}

                var log = $"【{DateTime.Now}——执行SQL】\r\n{UtilMethods.GetNativeSql(sql, pars)}\r\n";
                var originColor = Console.ForegroundColor;
                if (sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                    Console.ForegroundColor = ConsoleColor.Green;
                if (sql.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) || sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                    Console.ForegroundColor = ConsoleColor.Yellow;
                if (sql.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase))
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(log);
                Console.ForegroundColor = originColor;
                "SqlSugar".PrintToMiniProfiler("Info", log);
            };
            db.Aop.OnError = ex =>
            {
                if (ex.Parametres == null) return;
                var log = $"【{DateTime.Now}——错误SQL】\r\n{UtilMethods.GetNativeSql(ex.Sql, (SugarParameter[])ex.Parametres)}\r\n";
                var originColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(log);
                Console.ForegroundColor = originColor;
                "SqlSugar".PrintToMiniProfiler("Error", log);
            };
            db.Aop.OnLogExecuted = (sql, pars) =>
            {
                //// 若参数值超过100个字符则进行截取
                //foreach (var par in pars)
                //{
                //    if (par.DbType != System.Data.DbType.String || par.Value == null) continue;
                //    if (par.Value.ToString().Length > 100)
                //        par.Value = string.Concat(par.Value.ToString()[..100], "......");
                //}

                // 执行时间超过5秒时
                if (db.Ado.SqlExecutionTime.TotalSeconds > 5)
                {
                    var fileName = db.Ado.SqlStackTrace.FirstFileName; // 文件名
                    var fileLine = db.Ado.SqlStackTrace.FirstLine; // 行号
                    var firstMethodName = db.Ado.SqlStackTrace.FirstMethodName; // 方法名
                    var log = $"【{DateTime.Now}——超时SQL】\r\n【所在文件名】：{fileName}\r\n【代码行数】：{fileLine}\r\n【方法名】：{firstMethodName}\r\n" + $"【SQL语句】：{UtilMethods.GetNativeSql(sql, pars)}";
                    var originColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(log);
                    Console.ForegroundColor = originColor;
                    "SqlSugar".PrintToMiniProfiler("Slow", log);
                }
            };
        }
        // 数据审计
        db.Aop.DataExecuting = (oldValue, entityInfo) =>
        {
            // 新增/插入
            if (entityInfo.OperationType == DataFilterType.InsertByObject)
            {
                // 若主键是长整型且空则赋值雪花Id
                if (entityInfo.EntityColumnInfo.IsPrimarykey && entityInfo.EntityColumnInfo.PropertyInfo.PropertyType == typeof(long))
                {
                    var id = entityInfo.EntityColumnInfo.PropertyInfo.GetValue(entityInfo.EntityValue);
                    if (id == null || (long)id == 0)
                        entityInfo.SetValue(idGenerator.NextId());
                }
                // 若创建时间为空则赋值当前时间
                else if (entityInfo.PropertyName == nameof(EntityBase.CreateTime) && entityInfo.EntityColumnInfo.PropertyInfo.GetValue(entityInfo.EntityValue) == null)
                {
                    entityInfo.SetValue(DateTime.Now);
                }

                // 若当前用户非空（web线程时）
                if (httpContext != null && httpContext.User != null)
                {
                    if (entityInfo.PropertyName == nameof(EntityTenantId.TenantId))
                    {
                        var tenantId = ((dynamic)entityInfo.EntityValue).TenantId;
                        if (tenantId == null || tenantId == 0)
                            entityInfo.SetValue(httpContext.User.FindFirst(ClaimConst.TenantId)?.Value);
                    }
                    else if (entityInfo.PropertyName == nameof(EntityBase.CreateUserId))
                    {
                        var createUserId = ((dynamic)entityInfo.EntityValue).CreateUserId;
                        if (createUserId == 0 || createUserId == null)
                            entityInfo.SetValue(httpContext.User.FindFirst(ClaimConst.UserId)?.Value);
                    }
                    else if (entityInfo.PropertyName == nameof(EntityBase.CreateUserName))
                    {
                        var createUserName = ((dynamic)entityInfo.EntityValue).CreateUserName;
                        if (string.IsNullOrEmpty(createUserName))
                            entityInfo.SetValue(httpContext.User.FindFirst(ClaimConst.RealName)?.Value);
                    }
                    else if (entityInfo.PropertyName == nameof(EntityBaseData.CreateOrgId))
                    {
                        var createOrgId = ((dynamic)entityInfo.EntityValue).CreateOrgId;
                        if (createOrgId == 0 || createOrgId == null)
                            entityInfo.SetValue(httpContext.User.FindFirst(ClaimConst.OrgId)?.Value);
                    }
                    else if (entityInfo.PropertyName == nameof(EntityBaseData.CreateOrgName))
                    {
                        var createOrgName = ((dynamic)entityInfo.EntityValue).CreateOrgName;
                        if (string.IsNullOrEmpty(createOrgName))
                            entityInfo.SetValue(httpContext.User.FindFirst(ClaimConst.OrgName)?.Value);
                    }
                }
            }
            // 编辑/更新
            else if (entityInfo.OperationType == DataFilterType.UpdateByObject)
            {
                if (entityInfo.PropertyName == nameof(EntityBase.UpdateTime))
                    entityInfo.SetValue(DateTime.Now);
                else if (httpContext != null && entityInfo.PropertyName == nameof(EntityBase.UpdateUserId))
                    entityInfo.SetValue(httpContext.User?.FindFirst(ClaimConst.UserId)?.Value);
                else if (httpContext != null && entityInfo.PropertyName == nameof(EntityBase.UpdateUserName))
                    entityInfo.SetValue(httpContext.User?.FindFirst(ClaimConst.RealName)?.Value);
            }
        };

        // 超管排除其他过滤器
        if (httpContext != null && httpContext.User?.FindFirst(ClaimConst.AccountType)?.Value == ((int)AccountTypeEnum.SuperAdmin).ToString())
            return;

        // 配置假删除过滤器
        db.QueryFilter.AddTableFilter<IDeletedFilter>(u => u.IsDelete == false);

        // 配置租户过滤器
        var tenantId = httpContext?.User?.FindFirst(ClaimConst.TenantId)?.Value;
        if (!string.IsNullOrWhiteSpace(tenantId))
            db.QueryFilter.AddTableFilter<ITenantIdFilter>(u => u.TenantId == long.Parse(tenantId));

        // 配置用户机构（数据范围）过滤器
        sqlSugarFilter.SetOrgEntityFilter(db, httpContext);

        // 配置自定义过滤器
        sqlSugarFilter.SetCustomEntityFilter(db, httpContext, SqlSugarConst.MainConfigId);
    }

    #endregion
}
