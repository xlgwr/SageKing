using SageKing.Application.AspNetCore.SqlSugar.Service;
using SageKing.Cache.Features;
using SageKing.Database.SqlSugar.AspNetCore;
using SageKing.Database.SqlSugar.AspNetCore.Features;
using SageKing.Database.SqlSugar.Features;

namespace SageKing.Application.AspNetCore.SqlSugar.Features;


[DependsOn(typeof(SageKingCacheFeature))]
[DependsOn(typeof(SageKingSqlSugarAspNetCoreFeature))]
public class SageKingApplicationAspNetCoreSqlSugarFeature : FeatureBase
{
    public SageKingApplicationAspNetCoreSqlSugarFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// Represents the options for SageKingApplicationAspNetCoreSqlSugars feature.
    /// </summary>
    public Action<SageKingApplicationAspNetCoreSqlSugarOptions> DatabaseOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(DatabaseOptions)
            .AddSingleton<ISqlSugarAspNetCoreFilter, SqlSugarAspNetCoreFilter>();
    }
}
