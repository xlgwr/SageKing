using NewLife.Configuration;
using SageKing.Database.SqlSugar.Options;
using SageKing.Database.SqlSugar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.Features;

public class SageKingDatabaseSqlSugarFeature : FeatureBase
{
    public SageKingDatabaseSqlSugarFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// Represents the options for SageKingDatabaseSqlSugars feature.
    /// </summary>
    public Action<SageKingDatabaseSqlSugarOptions> ClientTypeDicOptions { get; set; } = _ => { };

    /// <summary>
    /// SqlSugarScope ConfigAction
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

    /// <inheritdoc />
    public override void Apply()
    {
        //工作单元IUnitOfWork放到具体应用中实现注入，如mvc,api,wpf等
        Services.Configure(ClientTypeDicOptions)
            .AddSingleton<Action<SageKingDatabaseSqlSugarOptions>>()
            .AddSingleton<ITenant, TenantScope>()
            .AddSingleton<ISqlSugarClient, TenantScope>()
            .AddScoped(typeof(SageKingRepository<>))
            .AddScoped(typeof(SageKingSqlSplitRepository<>));
    }
}
