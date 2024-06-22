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


    /// <inheritdoc />
    public override void Apply()
    {
        //工作单元IUnitOfWork放到具体应用中实现注入，如mvc,api,wpf等
        Services.Configure(ClientTypeDicOptions)
            .AddScoped(typeof(SageKingRepository<>))
            .AddScoped(typeof(SageKingSqlSplitRepository<>))
            .AddSingleton<ITenant, TenantScope>()
            .AddSingleton<ISqlSugarClient, TenantScope>();
    }
}
