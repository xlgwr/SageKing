using NewLife.Caching;
using SageKing.Application.AspNetCore.SqlSugar.Features;
using SageKing.Cache.Service;
using SageKing.Database.SqlSugar.AspNetCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Application.AspNetCore.SqlSugar.Service;

public class SqlSugarAspNetCoreFilter(SageKingCacheService sysCacheService) : ISqlSugarAspNetCoreFilter
{
    /// <summary>
    /// 缓存全局查询过滤器（内存缓存）
    /// </summary>
    private readonly ICache _cache = CacheDefault.Default;

    //当前程序集
    private IEnumerable<Type> _currAssemblyEffectiveTypes;

    /// <summary>
    ///  有组织，机构的去实现吧
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dbConfigId"></param>
    public virtual void DeleteUserOrgCache(long userId, string dbConfigId)
    {
        // 删除用户机构集合缓存
        sysCacheService.Remove($"{CacheConst.KeyUserOrg}{userId}");
        // 删除最大数据权限缓存
        sysCacheService.Remove($"{CacheConst.KeyRoleMaxDataScope}{userId}");
        // 删除用户机构（数据范围）缓存——过滤器
        _cache.Remove($"db:{dbConfigId}:orgList:{userId}");
    }

    public void SetCustomEntityFilter(SqlSugarScopeProvider db, HttpContext httpContent, string mainConfigId)
    {
        // 配置自定义缓存
        var userId = httpContent?.User?.FindFirst(ClaimConst.UserId)?.Value;
        var cacheKey = $"db:{db.CurrentConnectionConfig.ConfigId}:custom:{userId}";
        var tableFilterItemList = _cache.Get<List<TableFilterItem<object>>>(cacheKey);
        if (tableFilterItemList == null)
        {
            _currAssemblyEffectiveTypes ??= typeof(SageKingApplicationAspNetCoreSqlSugarFeature).Assembly.GetTypesSuppressSniffer(typeof(SuppressSnifferAttribute));
            // 获取自定义实体过滤器
            var entityFilterTypes = _currAssemblyEffectiveTypes.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass
                && u.GetInterfaces().Any(i => i.HasImplementedRawGeneric(typeof(IEntityFilter))));
            if (!entityFilterTypes.Any()) return;

            var tableFilterItems = new List<TableFilterItem<object>>();
            foreach (var entityFilter in entityFilterTypes)
            {
                var instance = Activator.CreateInstance(entityFilter);
                var entityFilterMethod = entityFilter.GetMethod("AddEntityFilter");
                var entityFilters = ((IList)entityFilterMethod?.Invoke(instance, null))?.Cast<object>();
                if (entityFilters == null) continue;

                foreach (var u in entityFilters)
                {
                    var tableFilterItem = (TableFilterItem<object>)u;
                    var entityType = tableFilterItem.GetType().GetProperty("type", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(tableFilterItem, null) as Type;
                    // 排除非当前数据库实体
                    var tAtt = entityType.GetCustomAttribute<TenantAttribute>();
                    if ((tAtt != null && db.CurrentConnectionConfig.ConfigId.ToString() != tAtt.configId.ToString()) ||
                        (tAtt == null && db.CurrentConnectionConfig.ConfigId.ToString() !=  mainConfigId))
                        continue;

                    tableFilterItems.Add(tableFilterItem);
                    db.QueryFilter.Add(tableFilterItem);
                }
            }
            _cache.Add(cacheKey, tableFilterItems);
        }
        else
        {
            tableFilterItemList.ForEach(u =>
            {
                db.QueryFilter.Add(u);
            });
        }
    }

    public int SetDataScopeFilter(SqlSugarScopeProvider db, HttpContext httpContent)
    {
        var maxDataScope = (int)DataScopeEnum.All;

        var userId = httpContent?.User?.FindFirst(ClaimConst.UserId)?.Value;
        if (string.IsNullOrWhiteSpace(userId)) return maxDataScope;


        // 获取用户最大数据范围---仅本人数据
        maxDataScope = _cache.Get<int>(CacheConst.KeyRoleMaxDataScope + userId);
        if (maxDataScope != (int)DataScopeEnum.Self) return maxDataScope;

        // 配置用户数据范围缓存
        var cacheKey = $"db:{db.CurrentConnectionConfig.ConfigId}:dataScope:{userId}";
        var dataScopeFilter = _cache.Get<ConcurrentDictionary<Type, LambdaExpression>>(cacheKey);
        if (dataScopeFilter == null)
        {
            _currAssemblyEffectiveTypes ??= typeof(SageKingApplicationAspNetCoreSqlSugarFeature).Assembly.GetTypesSuppressSniffer(typeof(SuppressSnifferAttribute));

            // 获取业务实体数据表
            var entityTypes = _currAssemblyEffectiveTypes.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass
                && u.IsSubclassOf(typeof(EntityBaseData)));
            if (!entityTypes.Any()) return maxDataScope;

            dataScopeFilter = new ConcurrentDictionary<Type, LambdaExpression>();
            foreach (var entityType in entityTypes)
            {
                // 排除非当前数据库实体
                var tAtt = entityType.GetCustomAttribute<TenantAttribute>();
                if ((tAtt != null && db.CurrentConnectionConfig.ConfigId.ToString() != tAtt.configId.ToString()))
                    continue;

                var lambda = DynamicExpressionParser.ParseLambda(new[] {
                    Expression.Parameter(entityType, "u") }, typeof(bool), $"u.{nameof(EntityBaseData.CreateUserId)}=@0", userId);
                db.QueryFilter.AddTableFilter(entityType, lambda);
                dataScopeFilter.TryAdd(entityType, lambda);
            }
            _cache.Add(cacheKey, dataScopeFilter);
        }
        else
        {
            foreach (var filter in dataScopeFilter)
                db.QueryFilter.AddTableFilter(filter.Key, filter.Value);
        }
        return maxDataScope;
    }
    /// <summary>
    /// 有组织，机构的去实现吧
    /// </summary>
    /// <param name="db"></param>
    /// <param name="httpContent"></param>
    public virtual void SetOrgEntityFilter(SqlSugarScopeProvider db, HttpContext httpContent)
    {
       
    }
}
