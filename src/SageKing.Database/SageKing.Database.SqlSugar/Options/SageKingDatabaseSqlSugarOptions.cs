using Microsoft.Extensions.Configuration;
using SageKing.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.Options;

/// <summary>
/// 配置字典
/// </summary>
public class SageKingDatabaseSqlSugarOptions:IOptionsBase
{ 
    /// <summary>
    /// Gets the <see cref="IServiceProvider"/>.
    /// </summary>
    public IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// 默认参数设置相关
    /// </summary>
    public SqlSugarDefaultSet SqlSugarDefault { get; set; }
    
    public DbConnectionOptions DBConnection { get; set; }

    /// <summary>
    /// SqlSugarScope ConfigAction and TenantScope
    /// SetDbAop,SetDbDiffLog
    /// </summary>
    public Action<SqlSugarClient> SqlSugarClientConfigAction { get; set; } = _ => { };

    /// <summary>
    /// 初始化数据库表结构及种子数据，到应用中去实现吧
    /// </summary>
    public Action<SqlSugarScope, DbConnectionConfig> InitDatabaseAction { get; set; } = (_, _) => { };

    /// <summary>
    /// 初始化租户业务数据库，到应用中去实现吧
    /// </summary>
    public Action<SqlSugarScope, DbConnectionConfig> InitTenantDatabaseAction { get; set; } = (_, _) => { };

    /// <summary>
    /// 多库切换相关回调处理
    /// ISqlSugarClient Context
    /// SqlSugarRepository<T> is T
    /// </summary>
    public Action<ISqlSugarClient, Type> ConnectionScopeAction { get; set; } = (_, _) => { };

    public string SectionName => "SageKingDatabaseSqlSugar";

    public void BindFromConfig(IConfigurationManager configurationManager)
    {
        configurationManager.GetSection(SectionName).Bind(this);
    }
}
